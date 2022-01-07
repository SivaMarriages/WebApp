import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BirthDetails, CandidateProfile, ProfileData } from '../models/profile';
import { ProfileService } from '../shared/profile.service';
import { UIService } from '../shared/UIService';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
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
    retStr += data.birthDetails.dateOfBirth;
    retStr += data.birthDetails.timeOfBirth;
    retStr += data.birthDetails.placeOfBirth;
    retStr += data.birthDetails.rasi;
    retStr += data.birthDetails.nakshatra;
    retStr += data.contactDetails.number1;
    retStr += data.contactDetails.number2;
    retStr += data.contactDetails.email;
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
    return `${data.dateOfBirth} ${data.timeOfBirth} ${data.placeOfBirth} ${data.rasi} ${data.nakshatra}`;
  }
  getParents(data: ProfileData): string {
    return `Father: ${data.father}, Mother:${data.mother}`;
  }
  getProfilePictureUrl(data: string[]) {
    return `api/profilePictures/${data[0]}`;
  }

  applyFilter(event: any) {
    const filter = event.target.value.toLowerCase();
    this.filterdProfiles = [];
    for (let i = 0; i < this.profiles.length; i++) {
      if (this.profilesDataStr[i].includes(filter)) {
        this.filterdProfiles.push(this.profiles[i]);
      }
    }
  }
}
