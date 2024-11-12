import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Hizmet } from './models/Hizmet';
import { HizmetService } from './services/Hizmet.service';
import { environment } from 'environments/environment';
import { Router } from '@angular/router';

declare var jQuery: any;

@Component({
	selector: 'app-hizmet',
	templateUrl: './hizmet.component.html',
	styleUrls: ['./hizmet.component.scss']
})
export class HizmetComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['hizmetId','photo','baslik','aciklama','sira', 'dil', 'update','delete','file','search'];

	hizmetList:Hizmet[];
	hizmet:Hizmet=new Hizmet();

	hizmetAddForm: FormGroup;
	photoForm: FormGroup;

	hizmetId:number;

	constructor(private router: Router, private hizmetService:HizmetService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getHizmetList();
    }

	ngOnInit() {

		this.createHizmetAddForm();
	}

	navigateToRotaPages(hizmetId: number) {
		this.router.navigate(['/hizmetpages', hizmetId]);
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
		formData.append('hizmetId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.hizmetService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getHizmetList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}

	getHizmetList() {
		this.hizmetService.getHizmetList().subscribe(data => {
			this.hizmetList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.hizmetAddForm.valid) {
			this.hizmet = Object.assign({}, this.hizmetAddForm.value)

			if (this.hizmet.hizmetId == 0)
				this.addHizmet();
			else
				this.updateHizmet();
		}

	}

	addHizmet(){

		this.hizmetService.addHizmet(this.hizmet).subscribe(data => {
			this.getHizmetList();
			this.hizmet = new Hizmet();
			jQuery('#hizmet').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hizmetAddForm);

		})

	}

	updateHizmet(){

		this.hizmetService.updateHizmet(this.hizmet).subscribe(data => {

			var index=this.hizmetList.findIndex(x=>x.hizmetId==this.hizmet.hizmetId);
			this.hizmetList[index]=this.hizmet;
			this.dataSource = new MatTableDataSource(this.hizmetList);
            this.configDataTable();
			this.hizmet = new Hizmet();
			jQuery('#hizmet').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hizmetAddForm);

		})

	}

	createHizmetAddForm() {
		this.hizmetAddForm = this.formBuilder.group({		
			hizmetId : [0],
photo : ["", Validators.required],
baslik : ["", Validators.required],
aciklama : ["", Validators.required],
sira : [0, Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteHizmet(hizmetId:number){
		this.hizmetService.deleteHizmet(hizmetId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.hizmetList=this.hizmetList.filter(x=> x.hizmetId!=hizmetId);
			this.dataSource = new MatTableDataSource(this.hizmetList);
			this.configDataTable();
		})
	}

	getHizmetById(hizmetId:number){
		this.clearFormGroup(this.hizmetAddForm);
		this.hizmetService.getHizmetById(hizmetId).subscribe(data=>{
			this.hizmet=data;
			this.hizmetAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'hizmetId')
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
