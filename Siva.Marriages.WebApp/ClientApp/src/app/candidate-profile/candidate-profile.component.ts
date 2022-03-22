import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CandidateProfile, ProfileData } from '../models/profile';
import { ProfileService, routesConstants, UIService } from '../shared';
import { Zodiac, Nakshatra, Marital, Elder } from '../models/profile';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-candidate-profile',
  templateUrl: './candidate-profile.component.html',
  styleUrls: ['./candidate-profile.component.css']
})
export class CandidateProfileComponent implements OnInit, OnDestroy {
  newProfile: boolean = false;
  editMode: boolean = false;
  sub: any;
  profileId: string = "";
  imageObject: { image: string, thumbImage: string }[] = [];

  zodiacSigns: string[];
  filterZodiacSigns: Observable<string[]>;
  nakshatras: string[];
  filterNakshatras: Observable<string[]>;
  maxYear: number = (new Date()).getFullYear() + 6;
  maritalStatuses: string[];
  elderStatuses: string[];

  profileData: ProfileData = <ProfileData>{};

  profileForm = this.fb.group({
    name: ['', Validators.required],
    surname: ['', Validators.required],
    motherMaidenname: [''],
    gender: ['', Validators.required],
    nativePlace: [''],
    contactDetails: this.fb.group({
      number1: ['', Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")],
      number2: ['', Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")],
      email: ['', Validators.email]
    }),
    birthDetails: this.fb.group({
      dateOfBirth: [''],
      timeOfBirth: [''],
      placeOfBirth: [''],
      rasi: [''],
      nakshatra: ['']
    }),
    profession: this.fb.group({
      designation: [''],
      companyName: [''],
      salary: [''],
      place: ['']
    }),
    education: this.fb.group({
      name: [''],
      institute: [''],
      location: ['']
    }),
    height: [''],
    otherDetails: [''],
    father: ['', Validators.required],
    mother: ['', Validators.required],
    siblings: this.fb.array([])
  });
  constructor(private fb: FormBuilder, private profileService: ProfileService, private router: Router, private route: ActivatedRoute, private uiService: UIService) {
    this.zodiacSigns = [];
    for (const [name, strValue] of Object.entries(Zodiac)) {
      const value = Number(strValue)
      if (Number.isNaN(value)) {
        continue;
      }
      this.zodiacSigns.push(name);
    }
    this.filterZodiacSigns = ((this.profileForm.get('birthDetails') as FormGroup).get('rasi') as FormControl).valueChanges.pipe(
      startWith(''),
      map(value => this.filterZodiacSign(value)),
    );

    this.nakshatras = [];
    for (const [name, strValue] of Object.entries(Nakshatra)) {
      const value = Number(strValue)
      if (Number.isNaN(value)) {
        continue;
      }
      this.nakshatras.push(name);
    }
    this.filterNakshatras = ((this.profileForm.get('birthDetails') as FormGroup).get('nakshatra') as FormControl).valueChanges.pipe(
      startWith(''),
      map(value => this.filterNakshatra(value)),
    );

    this.maritalStatuses = [];
    for (const [name, strValue] of Object.entries(Marital)) {
      const value = Number(strValue)
      if (Number.isNaN(value)) {
        continue;
      }
      this.maritalStatuses.push(name);
    }

    this.elderStatuses = [];
    for (const [name, strValue] of Object.entries(Elder)) {
      const value = Number(strValue)
      if (Number.isNaN(value)) {
        continue;
      }
      this.elderStatuses.push(name);
    }
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  async ngOnInit(): Promise<void> {
    this.sub = this.route.url.subscribe(async urlSegs => {
      this.uiService.showSpinner();
      if (urlSegs[0].path === 'new-profile') {
        this.newProfile = true;
        this.editMode = true;
      } else if (urlSegs[0].path === 'view-profile') {
        this.profileId = urlSegs[1].path;
        try {
          const profile = await this.profileService.GetProfile(this.profileId);
          this.resetSiblingsGroup();
          for (let i = 0; i < profile.data.siblings.length; i++) {
            this.addSibling();
          }
          this.profileData = profile.data;
          this.profileForm.setValue(profile.data);
          this.newProfile = false;
          this.editMode = false;
          this.profileForm.disable();
          await this.refreshImages(profile.picturesId);
        } catch (err) {
          this.uiService.showErrorToast(err)
        }
      } else if (urlSegs[0].path === 'edit-profile') {
        this.profileId = urlSegs[1].path;
        try {
          const profile = await this.profileService.GetProfile(this.profileId);
          this.resetSiblingsGroup();
          for (let i = 0; i < profile.data.siblings.length; i++) {
            this.addSibling();
          }
          this.profileData = profile.data;
          this.profileForm.setValue(profile.data);
          this.newProfile = false;
          this.editMode = true;
          this.profileForm.enable();
          this.refreshImages(profile.picturesId).then();
        } catch (err) {
          this.uiService.showErrorToast(err)
        };
      }
      this.uiService.stopSpinner();
    });
  }

  private async refreshImages(picturesId: string[]) {
    const tempImagesObject = [];
    for (let i = 0; i < picturesId.length; i++) {
      const picDataUri = await this.profileService.GetPicture(picturesId[i]);
      tempImagesObject.push({ image: picDataUri, thumbImage: picDataUri });
    }
    this.imageObject = tempImagesObject;
  }

  private filterZodiacSign(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.zodiacSigns.filter(zodiacSign => zodiacSign.toLowerCase().includes(filterValue));
  }

  private filterNakshatra(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.nakshatras.filter(nakshatra => nakshatra.toLowerCase().includes(filterValue));
  }

  get siblingsGroup() {
    return (this.profileForm.get('siblings') as FormArray).controls as FormGroup[];
  }

  resetSiblingsGroup(): void {
    this.profileForm.setControl('siblings', this.fb.array([]));
  }

  addSibling(): void {
    this.siblingsGroup.push(this.fb.group({
      details: ['', Validators.required]
    }));
  }

  deleteSibling(index: number): void {
    this.siblingsGroup.splice(index, 1);
  }

  async onEdit(): Promise<void> {
    await this.router.navigate([routesConstants.EDITPROFILE, this.profileId]);
  }

  async onEditPhotos(): Promise<void> {
    await this.router.navigate([routesConstants.PHOTOS, this.profileId]);
  }

  async onDelete(): Promise<void> {
    await this.profileService.DeleteProfile(this.profileId);
    await this.router.navigate([routesConstants.HOME]);
    this.uiService.showToast('Deleted Profile Successfully!');
  }

  async onSubmit(): Promise<void> {
    try {
      if (this.profileForm.valid &&
        ((this.profileForm.get('siblings') as FormArray).controls as FormGroup[]).every(ctr => {
          if (ctr.valid) {
            return true;
          } else {
            ctr.markAllAsTouched();
            return false;
          }
        })) {
        if (this.newProfile) {
          const profileId = await this.profileService.CreateProfile(this.profileForm.getRawValue());
          this.profileForm.reset();
          await this.router.navigate([routesConstants.VIEWPROFILE, profileId]);
        } else {
          await this.profileService.UpdateProfile(this.profileId, this.profileForm.getRawValue());
          this.profileForm.reset();
          await this.router.navigate([routesConstants.VIEWPROFILE, this.profileId]);
        }
        this.uiService.showToast('Saved Profile Successfully!');
      }
      else {
        this.uiService.showToast('Required Fields are Missing!');
      }
    }
    catch (excep) {
      throw excep;
    }
  }

  async onCancel(): Promise<void> {
    if (this.newProfile) {
      await this.router.navigate([routesConstants.HOME]);
    } else {
      await this.router.navigate([routesConstants.VIEWPROFILE, this.profileId]);
    }
  }

  async onReturn(): Promise<void> {
    await this.router.navigate([routesConstants.HOME]);
  }

  async onShare(): Promise<void> {
    try {
      this.uiService.showSpinner();
      let photos: File[] = [];
      const image = await fetch(this.imageObject[0].image);
      const blob = await image.blob();
      photos.push(new File([blob], `0.jpg`, { type: 'image/jpeg', endings: 'native' }));
      await window.navigator.share({ text: this.getShareData(this.profileData), files: photos });
    }
    catch (err) {
      this.uiService.showErrorToast(err);
    }
    finally {
      this.uiService.stopSpinner();
    }
  }

  get getNonPrimePhotos() {
    return this.imageObject.filter((x, i) => i !== 0);
  }

  get getHeader() {
    return [
      "🙏పరిచయం మాది వివరణామిది🙏",
      "🙏వధువు, వరుడు ల గుణ గుణాలు మరియు వారి ఫ్యామిలీ మంచి చెడులు తెలుసుకోవాల్సిన బాధ్యత మీది🙏"];
  }

  get getFooter() {
    return ["ఇట్లు,",
      "వల్లూరి శివ కుమార్ ప్రజాపతి - 9032055444",
      "పరిచయం మాది వివరణామిది",
      "వధువు, వరుడు ల గుణ గుణాలు మరియు వారి ఫ్యామిలీ మంచి చెడులు తెలుసుకోవాల్సిన బాధ్యత మీది"];
  }

  getShareData(profileData: ProfileData): string {
    let indentation = 0
    let linFn = (key:string, value:string):string => {
      return `${' '.repeat(indentation*4)}${key} : ${value}\n`;
    }
    let data = "";
    this.getHeader.forEach(header => {
      data += header + "\n\n\n\n\n";
    });
    data += linFn("Name", profileData.name);
    data += linFn("Surname", profileData.surname);
    data += linFn("MotherMaidenname", profileData.motherMaidenname);
    data += linFn("NativePlace", profileData.nativePlace);
    data += linFn("NativePlace", profileData.nativePlace);
    data += linFn("BirthDetails", "");
    indentation++;
    data += linFn("DateOfBirth", profileData.birthDetails.dateOfBirth);
    data += linFn("TimeOfBirth", profileData.birthDetails.dateOfBirth);
    data += linFn("PlaceOfBirth", profileData.birthDetails.dateOfBirth);
    data += linFn("Rasi", profileData.birthDetails.dateOfBirth);
    data += linFn("Nakshatra", profileData.birthDetails.dateOfBirth);
    indentation--;
    data += linFn("Profession", "");
    indentation++;
    data += linFn("Designation", profileData.profession.designation);
    data += linFn("CompanyName", profileData.profession.companyName);
    data += linFn("Salary", profileData.profession.salary);
    data += linFn("Place", profileData.profession.place);
    indentation--;
    data += linFn("Education", "");
    indentation++;
    data += linFn("Qualification", profileData.education.name);
    data += linFn("Institute", profileData.education.institute);
    data += linFn("Place", profileData.education.location);
    indentation--;
    data += linFn("Height", profileData.height);
    data += linFn("OtherDetails", profileData.otherDetails);
    data += "\n\n";
    data += linFn("Father", profileData.father);
    data += linFn("Mother", profileData.mother);
    data += "\n\n";
    profileData.siblings.forEach((sibling, idx) => {
      data += linFn(`Sibling ${idx + 1}`, sibling.details);
    });
    data += "\n\n\n\n\n"
    this.getFooter.forEach(header => {
      data += header + "\n";
    });
    return data
  }
}
