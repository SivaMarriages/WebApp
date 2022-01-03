import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BaseProfile, BirthDetails, CandidateProfile, ProfileData } from '../models/profile';
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
    retStr += data.gender;
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
    retStr += data.father.name;
    retStr += data.father.contactDetails.number1;
    retStr += data.father.contactDetails.number2;
    retStr += data.father.contactDetails.email;
    retStr += data.father.profession.designation;
    retStr += data.father.profession.companyName;
    retStr += data.father.profession.place;
    retStr += data.father.profession.salary;
    retStr += data.father.education.name;
    retStr += data.father.education.institute;
    retStr += data.father.education.location;
    retStr += data.father.otherDetails;
    retStr += data.mother.name;
    retStr += data.mother.contactDetails.number1;
    retStr += data.mother.contactDetails.number2;
    retStr += data.mother.contactDetails.email;
    retStr += data.mother.profession.designation;
    retStr += data.mother.profession.companyName;
    retStr += data.mother.profession.place;
    retStr += data.mother.profession.salary;
    retStr += data.mother.education.name;
    retStr += data.mother.education.institute;
    retStr += data.mother.education.location;
    retStr += data.mother.otherDetails;
    for (let sibling of data.siblings) {
      retStr += sibling.name;
      retStr += sibling.maritalStatus;
      retStr += sibling.elder;
      retStr += sibling.gender;
      retStr += sibling.contactDetails.number1;
      retStr += sibling.contactDetails.number2;
      retStr += sibling.contactDetails.email;
      retStr += sibling.profession.designation;
      retStr += sibling.profession.companyName;
      retStr += sibling.profession.place;
      retStr += sibling.profession.salary;
      retStr += sibling.education.name;
      retStr += sibling.education.institute;
      retStr += sibling.education.location;
      retStr += sibling.otherDetails;
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

  getCardTitle(data: ProfileData): string {
    return `${data.name} ${data.surname}` + (data.birthDetails && data.birthDetails.dateOfBirth ? `Age: ${(new Date()).getFullYear() - data.birthDetails.dateOfBirth.getFullYear()} years` : '');
  }
  getCardHeader(data: ProfileData): string {
    return `${data.profession.designation} ${data.profession.designation} ${data.profession.place} ${data.profession.salary} ${data.education.name}`;
  }
  getCardBirth(data: BirthDetails): string {
    return `${data.dateOfBirth} ${data.timeOfBirth} ${data.placeOfBirth} ${data.rasi} ${data.nakshatra}`;
  }
  getParents(data: ProfileData): string {
    return `Father: ${data.father.name}, Mother:${data.mother.name}`;
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
