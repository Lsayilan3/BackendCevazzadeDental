import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AnasayfaPhotoUrl } from './models/AnasayfaPhotoUrl';
import { AnasayfaPhotoUrlService } from './services/AnasayfaPhotoUrl.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-anasayfaPhotoUrl',
	templateUrl: './anasayfaPhotoUrl.component.html',
	styleUrls: ['./anasayfaPhotoUrl.component.scss']
})
export class AnasayfaPhotoUrlComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['anasayfaPhotoUrlId','photo', 'update','delete','file'];

	anasayfaPhotoUrlList:AnasayfaPhotoUrl[];
	anasayfaPhotoUrl:AnasayfaPhotoUrl=new AnasayfaPhotoUrl();

	anasayfaPhotoUrlAddForm: FormGroup;
	photoForm: FormGroup;

	anasayfaPhotoUrlId:number;

	constructor(private anasayfaPhotoUrlService:AnasayfaPhotoUrlService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getAnasayfaPhotoUrlList();
    }

	ngOnInit() {

		this.createAnasayfaPhotoUrlAddForm();
	}

	uploadFile(event) {
		const file = (event.target as HTMLInputElement).files[0];
		this.photoForm.patchValue({
		  file: file,
		});
		this.photoForm.get('file').updateValueAndValidity();
		
	  }

	upFile( id : number){
		this.photoForm = this.formBuilder.group({		
			id : [id],
	file : ["", Validators.required]
		})
	}

	addPhotoSave(){
		var formData: any = new FormData();
		formData.append('anasayfaPhotoUrlId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.anasayfaPhotoUrlService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getAnasayfaPhotoUrlList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}


	getAnasayfaPhotoUrlList() {
		this.anasayfaPhotoUrlService.getAnasayfaPhotoUrlList().subscribe(data => {
			this.anasayfaPhotoUrlList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.anasayfaPhotoUrlAddForm.valid) {
			this.anasayfaPhotoUrl = Object.assign({}, this.anasayfaPhotoUrlAddForm.value)

			if (this.anasayfaPhotoUrl.anasayfaPhotoUrlId == 0)
				this.addAnasayfaPhotoUrl();
			else
				this.updateAnasayfaPhotoUrl();
		}

	}

	addAnasayfaPhotoUrl(){

		this.anasayfaPhotoUrlService.addAnasayfaPhotoUrl(this.anasayfaPhotoUrl).subscribe(data => {
			this.getAnasayfaPhotoUrlList();
			this.anasayfaPhotoUrl = new AnasayfaPhotoUrl();
			jQuery('#anasayfaphotourl').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.anasayfaPhotoUrlAddForm);

		})

	}

	updateAnasayfaPhotoUrl(){

		this.anasayfaPhotoUrlService.updateAnasayfaPhotoUrl(this.anasayfaPhotoUrl).subscribe(data => {

			var index=this.anasayfaPhotoUrlList.findIndex(x=>x.anasayfaPhotoUrlId==this.anasayfaPhotoUrl.anasayfaPhotoUrlId);
			this.anasayfaPhotoUrlList[index]=this.anasayfaPhotoUrl;
			this.dataSource = new MatTableDataSource(this.anasayfaPhotoUrlList);
            this.configDataTable();
			this.anasayfaPhotoUrl = new AnasayfaPhotoUrl();
			jQuery('#anasayfaphotourl').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.anasayfaPhotoUrlAddForm);

		})

	}

	createAnasayfaPhotoUrlAddForm() {
		this.anasayfaPhotoUrlAddForm = this.formBuilder.group({		
			anasayfaPhotoUrlId : [0],
photo : ["", Validators.required]
		})
	}

	deleteAnasayfaPhotoUrl(anasayfaPhotoUrlId:number){
		this.anasayfaPhotoUrlService.deleteAnasayfaPhotoUrl(anasayfaPhotoUrlId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.anasayfaPhotoUrlList=this.anasayfaPhotoUrlList.filter(x=> x.anasayfaPhotoUrlId!=anasayfaPhotoUrlId);
			this.dataSource = new MatTableDataSource(this.anasayfaPhotoUrlList);
			this.configDataTable();
		})
	}

	getAnasayfaPhotoUrlById(anasayfaPhotoUrlId:number){
		this.clearFormGroup(this.anasayfaPhotoUrlAddForm);
		this.anasayfaPhotoUrlService.getAnasayfaPhotoUrlById(anasayfaPhotoUrlId).subscribe(data=>{
			this.anasayfaPhotoUrl=data;
			this.anasayfaPhotoUrlAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'anasayfaPhotoUrlId')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
