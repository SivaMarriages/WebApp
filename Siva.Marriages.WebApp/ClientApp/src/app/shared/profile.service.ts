import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CandidateProfile, ProfileData } from '../models/profile';
import { UIService } from './';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private profile: CandidateProfile[] = [];
  private cachedPictures:{id:string, pic:any}[] = [];
  constructor(private http: HttpClient, private uiService: UIService) {
  }

  public async GetAllProfile(latest?: boolean): Promise<CandidateProfile[]> {
    if (latest || this.profile.length == 0) {
      this.uiService.showSpinner();
      try {
        this.profile = await this.http.get<CandidateProfile[]>('api/profile').toPromise();
      }
      finally {
        this.uiService.stopSpinner();
      }
      return this.profile;
    }
    else {
      return this.profile;
    }
  }

  public async GetProfile(id: string): Promise<CandidateProfile> {
    this.uiService.showSpinner();
    let ret
    try {
      ret = await this.http.get<CandidateProfile>('api/profile/' + encodeURIComponent(id)).toPromise();
    } finally {
      this.uiService.stopSpinner();
    }
    return ret;
  }

  public async CreateProfile(profileData: ProfileData): Promise<string> {
    this.uiService.showSpinner();
    try {
      return await this.http.post<string>('api/profile/', profileData).toPromise();
    } finally {
      this.uiService.stopSpinner();
    }
  }

  public async UpdateProfile(id: string, profileData: ProfileData): Promise<void> {
    this.uiService.showSpinner();
    try {
      await this.http.put('api/profile/' + encodeURIComponent(id), profileData).toPromise();
    } finally {
      this.uiService.stopSpinner();
    }
  }

  public async DeleteProfile(id: string): Promise<void> {
    this.uiService.showSpinner();
    try {
      await this.http.delete('api/profile/' + encodeURIComponent(id)).toPromise();
    } finally {
      this.uiService.stopSpinner();
    }
  }

  public async GetPicture(id: string): Promise<any> {
    for(let i = 0; i < this.cachedPictures.length; i++){
      if(this.cachedPictures[i].id === id){
        return this.cachedPictures[i].pic;
      }
    }
    const blob = await this.http.get('api/ProfilePictures/' + encodeURIComponent(id), {
      responseType: "blob"
    }).toPromise();
    let cachePic = {id:id, pic:await this.ConvertBlobToDataUri(blob)};
    this.cachedPictures.push(cachePic);
    return cachePic.pic;
  }

  public async MakePictureAsPrimary(id: string, pictureId: string): Promise<string[]> {
    this.uiService.showSpinner();
    try {
      return await this.http.get<string[]>('api/ProfilePictures/' + encodeURIComponent(id) + '/' + encodeURIComponent(pictureId)).toPromise();
    } finally {
      this.uiService.stopSpinner();
    }
  }

  public async SavePictureToProfile(id: string, formData: FormData): Promise<void> {
    this.uiService.showSpinner();
    try {
      await this.http.post('api/ProfilePictures/' + encodeURIComponent(id), formData).toPromise();
    } finally {
      this.uiService.stopSpinner();
    }
  }

  public async DeletePictureFromProfile(id: string, pictureId: string): Promise<void> {
    this.uiService.showSpinner();
    try {
      await this.http.delete('api/ProfilePictures/' + encodeURIComponent(id) + '/' + encodeURIComponent(pictureId)).toPromise();
    } finally {
      this.uiService.stopSpinner();
    }
  }

  private ConvertBlobToDataUri(blob: Blob): Promise<any> {
    return new Promise(function (resolve) {
      const reader = new FileReader();
      reader.onloadend = () => {
        resolve(reader.result);
      };
      reader.readAsDataURL(blob);
    });
  }
}
