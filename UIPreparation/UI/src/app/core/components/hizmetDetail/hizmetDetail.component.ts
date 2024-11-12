import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { HizmetDetail } from './models/HizmetDetail';
import { HizmetDetailService } from './services/HizmetDetail.service';
import { environment } from 'environments/environment';
import { Hizmet } from '../hizmet/models/Hizmet';
import { HizmetService } from '../hizmet/services/Hizmet.service';

declare var jQuery: any;

@Component({
	selector: 'app-hizmetDetail',
	templateUrl: './hizmetDetail.component.html',
	styleUrls: ['./hizmetDetail.component.scss']
})
export class HizmetDetailComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['hizmetDetailId','hizmetId','photo','editor', 'dil','update','delete','file'];

	hizmetDetailList:HizmetDetail[];
	hizmetDetail:HizmetDetail=new HizmetDetail();

	hizmetDetailAddForm: FormGroup;
	photoForm: FormGroup;

	hizmetList:Hizmet[];
	hizmetDetailId:number;

	constructor(private hizmetService: HizmetService, private hizmetDetailService:HizmetDetailService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
		this.hizmetService.getHizmetList().subscribe(data=>this.hizmetList=data);
        this.getHizmetDetailList();
    }

	ngOnInit() {
		this.hizmetService.getHizmetList().subscribe(data=>this.hizmetList=data);
		this.createHizmetDetailAddForm();
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
		formData.append('hizmetDetailId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.hizmetDetailService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getHizmetDetailList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}


	getHizmetDetailList() {
		this.hizmetDetailService.getHizmetDetailList().subscribe(data => {
			this.hizmetDetailList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.hizmetDetailAddForm.valid) {
			this.hizmetDetail = Object.assign({}, this.hizmetDetailAddForm.value)

			if (this.hizmetDetail.hizmetDetailId == 0)
				this.addHizmetDetail();
			else
				this.updateHizmetDetail();
		}

	}

	addHizmetDetail(){

		this.hizmetDetailService.addHizmetDetail(this.hizmetDetail).subscribe(data => {
			this.getHizmetDetailList();
			this.hizmetDetail = new HizmetDetail();
			jQuery('#hizmetdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hizmetDetailAddForm);

		})

	}

	updateHizmetDetail(){

		this.hizmetDetailService.updateHizmetDetail(this.hizmetDetail).subscribe(data => {

			var index=this.hizmetDetailList.findIndex(x=>x.hizmetDetailId==this.hizmetDetail.hizmetDetailId);
			this.hizmetDetailList[index]=this.hizmetDetail;
			this.dataSource = new MatTableDataSource(this.hizmetDetailList);
            this.configDataTable();
			this.hizmetDetail = new HizmetDetail();
			jQuery('#hizmetdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hizmetDetailAddForm);

		})

	}

	createHizmetDetailAddForm() {
		this.hizmetDetailAddForm = this.formBuilder.group({		
			hizmetDetailId : [0],
hizmetId : [0, Validators.required],
photoService : ["", Validators.required],
photo : ["", Validators.required],
editor : ["", Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteHizmetDetail(hizmetDetailId:number){
		this.hizmetDetailService.deleteHizmetDetail(hizmetDetailId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.hizmetDetailList=this.hizmetDetailList.filter(x=> x.hizmetDetailId!=hizmetDetailId);
			this.dataSource = new MatTableDataSource(this.hizmetDetailList);
			this.configDataTable();
		})
	}
//burası değişen
	getHizmetDetailiById(hizmetDetailId:number){
		this.clearFormGroup(this.hizmetDetailAddForm);
		this.hizmetDetailService.getHizmetDetailiById(hizmetDetailId).subscribe(data=>{
			this.hizmetDetail=data;
			this.hizmetDetailAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'hizmetDetailId')
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
