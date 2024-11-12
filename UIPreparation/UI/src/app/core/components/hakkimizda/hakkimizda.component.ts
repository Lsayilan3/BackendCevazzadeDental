import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Hakkimizda } from './models/Hakkimizda';
import { HakkimizdaService } from './services/Hakkimizda.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-hakkimizda',
	templateUrl: './hakkimizda.component.html',
	styleUrls: ['./hakkimizda.component.scss']
})
export class HakkimizdaComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['hakkimizdaId','photo','editor','dil', 'update','delete','file'];

	hakkimizdaList:Hakkimizda[];
	hakkimizda:Hakkimizda=new Hakkimizda();

	hakkimizdaAddForm: FormGroup;
	photoForm: FormGroup;


	hakkimizdaId:number;

	constructor(private hakkimizdaService:HakkimizdaService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getHakkimizdaList();
    }

	ngOnInit() {

		this.createHakkimizdaAddForm();
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
		formData.append('hakkimizdaId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.hakkimizdaService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getHakkimizdaList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}

	getHakkimizdaList() {
		this.hakkimizdaService.getHakkimizdaList().subscribe(data => {
			this.hakkimizdaList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.hakkimizdaAddForm.valid) {
			this.hakkimizda = Object.assign({}, this.hakkimizdaAddForm.value)

			if (this.hakkimizda.hakkimizdaId == 0)
				this.addHakkimizda();
			else
				this.updateHakkimizda();
		}

	}

	addHakkimizda(){

		this.hakkimizdaService.addHakkimizda(this.hakkimizda).subscribe(data => {
			this.getHakkimizdaList();
			this.hakkimizda = new Hakkimizda();
			jQuery('#hakkimizda').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hakkimizdaAddForm);

		})

	}

	updateHakkimizda(){

		this.hakkimizdaService.updateHakkimizda(this.hakkimizda).subscribe(data => {

			var index=this.hakkimizdaList.findIndex(x=>x.hakkimizdaId==this.hakkimizda.hakkimizdaId);
			this.hakkimizdaList[index]=this.hakkimizda;
			this.dataSource = new MatTableDataSource(this.hakkimizdaList);
            this.configDataTable();
			this.hakkimizda = new Hakkimizda();
			jQuery('#hakkimizda').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hakkimizdaAddForm);

		})

	}

	createHakkimizdaAddForm() {
		this.hakkimizdaAddForm = this.formBuilder.group({		
			hakkimizdaId : [0],
photo : ["", Validators.required],
editor : ["", Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteHakkimizda(hakkimizdaId:number){
		this.hakkimizdaService.deleteHakkimizda(hakkimizdaId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.hakkimizdaList=this.hakkimizdaList.filter(x=> x.hakkimizdaId!=hakkimizdaId);
			this.dataSource = new MatTableDataSource(this.hakkimizdaList);
			this.configDataTable();
		})
	}

	getHakkimizdaById(hakkimizdaId:number){
		this.clearFormGroup(this.hakkimizdaAddForm);
		this.hakkimizdaService.getHakkimizdaById(hakkimizdaId).subscribe(data=>{
			this.hakkimizda=data;
			this.hakkimizdaAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'hakkimizdaId')
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
