import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Hekim } from './models/Hekim';
import { HekimService } from './services/Hekim.service';
import { environment } from 'environments/environment';
import { Router } from '@angular/router';

declare var jQuery: any;

@Component({
	selector: 'app-hekim',
	templateUrl: './hekim.component.html',
	styleUrls: ['./hekim.component.scss']
})
export class HekimComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['hekimId','photo','ad','uzmanlik','aciklama','sosyalFace','sosyalTwitter','sosyalingstagram','sosyalMail', 'dil','update','delete','file','search'];

	hekimList:Hekim[];
	hekim:Hekim=new Hekim();

	hekimAddForm: FormGroup;
	photoForm: FormGroup;


	hekimId:number;

	constructor(private router: Router,private hekimService:HekimService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getHekimList();
    }

	ngOnInit() {

		this.createHekimAddForm();
	}


	navigateToRotaPages(hekimId: number) {
		this.router.navigate(['/hekimpages', hekimId]);
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
		formData.append('hekimId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.hekimService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getHekimList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}


	getHekimList() {
		this.hekimService.getHekimList().subscribe(data => {
			this.hekimList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.hekimAddForm.valid) {
			this.hekim = Object.assign({}, this.hekimAddForm.value)

			if (this.hekim.hekimId == 0)
				this.addHekim();
			else
				this.updateHekim();
		}

	}

	addHekim(){

		this.hekimService.addHekim(this.hekim).subscribe(data => {
			this.getHekimList();
			this.hekim = new Hekim();
			jQuery('#hekim').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hekimAddForm);

		})

	}

	updateHekim(){

		this.hekimService.updateHekim(this.hekim).subscribe(data => {

			var index=this.hekimList.findIndex(x=>x.hekimId==this.hekim.hekimId);
			this.hekimList[index]=this.hekim;
			this.dataSource = new MatTableDataSource(this.hekimList);
            this.configDataTable();
			this.hekim = new Hekim();
			jQuery('#hekim').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hekimAddForm);

		})

	}

	createHekimAddForm() {
		this.hekimAddForm = this.formBuilder.group({		
			hekimId : [0],
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

	deleteHekim(hekimId:number){
		this.hekimService.deleteHekim(hekimId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.hekimList=this.hekimList.filter(x=> x.hekimId!=hekimId);
			this.dataSource = new MatTableDataSource(this.hekimList);
			this.configDataTable();
		})
	}

	getHekimById(hekimId:number){
		this.clearFormGroup(this.hekimAddForm);
		this.hekimService.getHekimById(hekimId).subscribe(data=>{
			this.hekim=data;
			this.hekimAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'hekimId')
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
