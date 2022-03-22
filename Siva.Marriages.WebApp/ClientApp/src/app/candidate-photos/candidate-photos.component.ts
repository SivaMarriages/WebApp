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
  constructor(private route:ActivatedRoute, private profileService: ProfileService, private uiService:UIService,private router: Router){

  }

  async ngOnInit(): Promise<void> {
    let urlSegs = this.route.snapshot.url;
    this.profileId = urlSegs[1].path;
    this.picturesId = (await this.profileService.GetProfile(this.profileId)).picturesId;
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
        await this.ngOnInit();
        this.uiService.showToast("Photo Uploaded Successfully!");
    }
    event.target.value='';
  }

}
