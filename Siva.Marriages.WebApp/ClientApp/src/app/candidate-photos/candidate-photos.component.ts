import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ProfileService } from "../shared/profile.service";
import { routesConstants } from "../shared/routes.constants";
import { UIService } from "../shared/UIService";

@Component({
  selector: 'app-candidate-photos',
  templateUrl: './candidate-photos.component.html'
})
export class CandidatePhotosComponent implements OnInit, OnDestroy{
  sub:any;
  profileId:string = '';
  picturesId:string[] = [];
  constructor(private route:ActivatedRoute, private profileService: ProfileService, private uiService:UIService,private router: Router){

  }

  ngOnInit(): void {
    this.sub = this.route.url.subscribe(urlSegs => {
      this.profileId = urlSegs[1].path;
      this.profileService.GetProfile(this.profileId).then(p => this.picturesId = p.picturesId, err => this.uiService.showToast(err));
    });
  }
  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  async closePhotosEdit():Promise<void>{
    this.router.navigate([routesConstants.VIEWPROFILE, this.profileId]);
  }

  async onPrimarySet(pictureId:string):Promise<void>{
    this.picturesId = (await this.profileService.MakePictureAsPrimary(this.profileId, pictureId));
  }

  async deletePhoto(pictureIdx:number):Promise<void>{
    await this.profileService.DeletePictureFromProfile(this.profileId, this.picturesId[pictureIdx]);
    this.picturesId.splice(pictureIdx, 1);
  }

  async fileAdded(event: any):Promise<void>{
    let fileList: FileList = event.target.files;
    if(fileList.length > 0) {
        let file: File = fileList[0];
        let formData:FormData = new FormData();
        formData.append('uploadFile', file, file.name);
        await this.profileService.SavePictureToProfile(this.profileId, formData);
        await this.router.navigate([routesConstants.PHOTOS, this.profileId])
        this.uiService.showToast("Photo Uploaded Successfully!");
    }
  }

}
