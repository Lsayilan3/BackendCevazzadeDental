import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Durum } from './models/Durum';
import { DurumService } from './services/Durum.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-durum',
	templateUrl: './durum.component.html',
	styleUrls: ['./durum.component.scss']
})
export class DurumComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['durumId','baslik','paragrafOne','paragrafTwo','photo','dil', 'update','delete','file'];

	durumList:Durum[];
	durum:Durum=new Durum();

	durumAddForm: FormGroup;
	photoForm: FormGroup;

	durumId:number;

	constructor(private durumService:DurumService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getDurumList();
    }

	ngOnInit() {

		this.createDurumAddForm();
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
		formData.append('durumId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.durumService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getDurumList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}



	getDurumList() {
		this.durumService.getDurumList().subscribe(data => {
			this.durumList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.durumAddForm.valid) {
			this.durum = Object.assign({}, this.durumAddForm.value)

			if (this.durum.durumId == 0)
				this.addDurum();
			else
				this.updateDurum();
		}

	}

	addDurum(){

		this.durumService.addDurum(this.durum).subscribe(data => {
			this.getDurumList();
			this.durum = new Durum();
			jQuery('#durum').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.durumAddForm);

		})

	}

	updateDurum(){

		this.durumService.updateDurum(this.durum).subscribe(data => {

			var index=this.durumList.findIndex(x=>x.durumId==this.durum.durumId);
			this.durumList[index]=this.durum;
			this.dataSource = new MatTableDataSource(this.durumList);
            this.configDataTable();
			this.durum = new Durum();
			jQuery('#durum').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.durumAddForm);

		})

	}

	createDurumAddForm() {
		this.durumAddForm = this.formBuilder.group({		
			durumId : [0],
baslik : ["", Validators.required],
paragrafOne : ["", Validators.required],
paragrafTwo : ["", Validators.required],
photo : ["", Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteDurum(durumId:number){
		this.durumService.deleteDurum(durumId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.durumList=this.durumList.filter(x=> x.durumId!=durumId);
			this.dataSource = new MatTableDataSource(this.durumList);
			this.configDataTable();
		})
	}

	getDurumById(durumId:number){
		this.clearFormGroup(this.durumAddForm);
		this.durumService.getDurumById(durumId).subscribe(data=>{
			this.durum=data;
			this.durumAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'durumId')
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
