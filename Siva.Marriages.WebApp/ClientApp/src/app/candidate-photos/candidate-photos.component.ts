import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { UIService, ProfileService, routesConstants } from "../shared";

@Component({
  selector: 'app-candidate-photos',
  templateUrl: './candidate-photos.component.html',
  styleUrls: ['./candidate-photos.component.css']
})
export class CandidatePhotosComponent implements OnInit{
  profileId:string = '';
  picturesId:string[] = [];
  pictures:any[] = [];
  constructor(private route:ActivatedRoute, private profileService: ProfileService, private uiService:UIService,private router: Router){

  }

  async ngOnInit(): Promise<void> {
    this.uiService.showSpinner();
    let urlSegs = this.route.snapshot.url;
    this.profileId = urlSegs[1].path;
    this.picturesId = (await this.profileService.GetProfile(this.profileId)).picturesId;
    const promises = this.picturesId.map(async id => await this.profileService.GetPicture(id));
    this.pictures = await Promise.all(promises);
    this.uiService.stopSpinner();
  }

  async closePhotosEdit():Promise<void>{
    this.router.navigate([routesConstants.VIEWPROFILE, this.profileId]);
  }

  async onPrimarySet(idx:number):Promise<void>{
    this.picturesId = (await this.profileService.MakePictureAsPrimary(this.profileId, this.picturesId[idx]));
    const promises = this.picturesId.map(async id => await this.profileService.GetPicture(id));
    this.pictures = await Promise.all(promises);
  }

  async deletePhoto(pictureIdx:number):Promise<void>{
    await this.profileService.DeletePictureFromProfile(this.profileId, this.picturesId[pictureIdx]);
    this.picturesId.splice(pictureIdx, 1);
    this.pictures.splice(pictureIdx, 1);
  }

  async fileAdded(event: any):Promise<void>{
    let fileList: FileList = event.target.files;
    if(fileList.length > 0) {
        let file: File = fileList[0];
        let formData:FormData = new FormData();
        formData.append('uploadFile', file, file.name);
        await this.profileService.SavePictureToProfile(this.profileId, formData);
        await this.ngOnInit();
        this.uiService.showToast("Photo Uploaded Successfully!");
    }
    event.target.value='';
  }
}
