import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BirthDetails, CandidateProfile, Profession, ProfileData } from '../models/profile';
import { ProfileService } from '../shared/profile.service';
import { UIService } from '../shared/UIService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  filterText = "";
  profiles: CandidateProfile[];
  filterdProfiles: CandidateProfile[];
  profilesDataStr: string[];
  dataStr(data: ProfileData): string {
    let retStr = "";
    retStr += data.name;
    retStr += data.surname;
    retStr += data.motherMaidenname;
    retStr += data.nativePlace;
    retStr += data.gender.toString();
    retStr += data.contactDetails.number1;
    retStr += data.contactDetails.number2;
    retStr += data.contactDetails.email;
    retStr += data.birthDetails.dateOfBirth;
    retStr += data.birthDetails.timeOfBirth;
    retStr += data.birthDetails.placeOfBirth;
    retStr += data.birthDetails.rasi;
    retStr += data.birthDetails.nakshatra;
    retStr += data.profession.designation;
    retStr += data.profession.companyName;
    retStr += data.profession.place;
    retStr += data.profession.salary;
    retStr += data.education.name;
    retStr += data.education.institute;
    retStr += data.education.location;
    retStr += data.height;
    retStr += data.otherDetails;
    retStr += data.father;
    retStr += data.mother;
    for (let sibling of data.siblings) {
      retStr += sibling.details;
    }
    return retStr.toLowerCase();
  }
  constructor(private profileService: ProfileService, private router: Router, private uiService: UIService) {
    this.profiles = [];
    this.profilesDataStr = [];
    this.filterdProfiles = [];
    this.profileService.GetAllProfile(true).then(data => {
      this.profiles = data;
      this.filterdProfiles = data;
      this.profilesDataStr = data.map(p => this.dataStr(p.data));
    }, err => this.uiService.showToast(err));
  }

  getProfileAge(data: ProfileData): string {
    return (data.birthDetails && data.birthDetails.dateOfBirth ? `Age: ${(new Date()).getFullYear() - (new Date(data.birthDetails.dateOfBirth)).getFullYear()} years` : '');
  }
  getCardBirth(data: BirthDetails): string {
    let ret = "";
    if (data.dateOfBirth !== "") {
      ret += (new Date(data.dateOfBirth)).toDateString() + ", ";
    }
    if (data.timeOfBirth !== "") {
      ret += data.timeOfBirth + ", ";
    }
    if (data.rasi !== undefined && data.rasi.toString() !== "") {
      ret += data.rasi + ", ";
    }
    if (data.nakshatra !== undefined && data.nakshatra.toString() !== "") {
      ret += data.nakshatra + ", ";
    }

    if (ret.length > 0) {
      ret = ret.slice(0, ret.length - 2)
    }
    return ret;
  }
  getWorkDetails(profession: Profession): string {
    let ret = "";
    if (profession.designation !== "") {
      ret += profession.designation + ", ";
    }
    if (profession.companyName !== "") {
      ret += profession.companyName + ", ";
    }
    if (profession.place !== "") {
      ret += profession.place + ", ";
    }

    if (ret.length > 0) {
      ret = ret.slice(0, ret.length - 2)
    }
    return ret;
  }
  getProfilePictureUrl(data: string[]) {
    return `api/profilePictures/${data[0]}`;
  }

  applyFilter() {
    const filter = this.filterText.toLowerCase();
    this.filterdProfiles = [];
    for (let i = 0; i < this.profiles.length; i++) {
      if (this.profilesDataStr[i].includes(filter)) {
        this.filterdProfiles.push(this.profiles[i]);
      }
    }
  }
}
