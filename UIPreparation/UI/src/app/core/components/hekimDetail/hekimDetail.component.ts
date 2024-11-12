import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { HekimDetail } from './models/HekimDetail';
import { HekimDetailService } from './services/HekimDetail.service';
import { environment } from 'environments/environment';
import { HekimService } from '../hekim/services/Hekim.service';
import { Hekim } from '../hekim/models/Hekim';

declare var jQuery: any;

@Component({
	selector: 'app-hekimDetail',
	templateUrl: './hekimDetail.component.html',
	styleUrls: ['./hekimDetail.component.scss']
})
export class HekimDetailComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['hekimId','photo','ad','uzmanlik','aciklama','sosyalFace','sosyalTwitter','sosyalingstagram','sosyalMail','dil','update','delete','file'];

	hekimDetailList:HekimDetail[];
	hekimDetail:HekimDetail=new HekimDetail();

	hekimDetailAddForm: FormGroup;
	photoForm: FormGroup;

	hekimList:Hekim[];
	hekimDetailId:number;

	constructor(private hekimSerivce:HekimService, private hekimDetailService:HekimDetailService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
		this.hekimSerivce.getHekimList().subscribe(data=>this.hekimList=data);
        this.getHekimDetailList();
    }

	ngOnInit() {
		this.hekimSerivce.getHekimList().subscribe(data=>this.hekimList=data);
		this.createHekimDetailAddForm();
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
		formData.append('hekimDetailId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.hekimDetailService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getHekimDetailList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}


	getHekimDetailList() {
		this.hekimDetailService.getHekimDetailList().subscribe(data => {
			this.hekimDetailList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.hekimDetailAddForm.valid) {
			this.hekimDetail = Object.assign({}, this.hekimDetailAddForm.value)

			if (this.hekimDetail.hekimDetailId == 0)
				this.addHekimDetail();
			else
				this.updateHekimDetail();
		}

	}

	addHekimDetail(){

		this.hekimDetailService.addHekimDetail(this.hekimDetail).subscribe(data => {
			this.getHekimDetailList();
			this.hekimDetail = new HekimDetail();
			jQuery('#hekimdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hekimDetailAddForm);

		})

	}

	updateHekimDetail(){

		this.hekimDetailService.updateHekimDetail(this.hekimDetail).subscribe(data => {

			var index=this.hekimDetailList.findIndex(x=>x.hekimDetailId==this.hekimDetail.hekimDetailId);
			this.hekimDetailList[index]=this.hekimDetail;
			this.dataSource = new MatTableDataSource(this.hekimDetailList);
            this.configDataTable();
			this.hekimDetail = new HekimDetail();
			jQuery('#hekimdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hekimDetailAddForm);

		})

	}

	createHekimDetailAddForm() {
		this.hekimDetailAddForm = this.formBuilder.group({		
			hekimDetailId : [0],
hekimId : [0, Validators.required],
photo : ["", Validators.required],
ad : ["", Validators.required],
uzmanlik : ["", Validators.required],
aciklama : ["", Validators.required],
sosyalFace : ["", Validators.required],
sosyalTwitter : ["", Validators.required],
sosyalingstagram : ["", Validators.required],
sosyalMail : ["", Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteHekimDetail(hekimDetailId:number){
		this.hekimDetailService.deleteHekimDetail(hekimDetailId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.hekimDetailList=this.hekimDetailList.filter(x=> x.hekimDetailId!=hekimDetailId);
			this.dataSource = new MatTableDataSource(this.hekimDetailList);
			this.configDataTable();
		})
	}

	getHekimDetailiById(hekimDetailId:number){
		this.clearFormGroup(this.hekimDetailAddForm);
		this.hekimDetailService.getHekimDetailiById(hekimDetailId).subscribe(data=>{
			this.hekimDetail=data;
			this.hekimDetailAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'hekimDetailId')
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
