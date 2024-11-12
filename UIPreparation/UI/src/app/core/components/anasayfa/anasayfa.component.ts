import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Anasayfa } from './models/Anasayfa';
import { AnasayfaService } from './services/Anasayfa.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-anasayfa',
	templateUrl: './anasayfa.component.html',
	styleUrls: ['./anasayfa.component.scss']
})
export class AnasayfaComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['anasayfaId','baslik','baslikYazi','aciklama','photo', 'dil', 'update','delete','file'];

	anasayfaList:Anasayfa[];
	anasayfa:Anasayfa=new Anasayfa();

	anasayfaAddForm: FormGroup;
	photoForm: FormGroup;

	anasayfaId:number;

	constructor(private anasayfaService:AnasayfaService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getAnasayfaList();
    }

	ngOnInit() {

		this.createAnasayfaAddForm();
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
		formData.append('anasayfaId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.anasayfaService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getAnasayfaList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}


	getAnasayfaList() {
		this.anasayfaService.getAnasayfaList().subscribe(data => {
			this.anasayfaList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.anasayfaAddForm.valid) {
			this.anasayfa = Object.assign({}, this.anasayfaAddForm.value)

			if (this.anasayfa.anasayfaId == 0)
				this.addAnasayfa();
			else
				this.updateAnasayfa();
		}

	}

	addAnasayfa(){

		this.anasayfaService.addAnasayfa(this.anasayfa).subscribe(data => {
			this.getAnasayfaList();
			this.anasayfa = new Anasayfa();
			jQuery('#anasayfa').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.anasayfaAddForm);

		})

	}

	updateAnasayfa(){

		this.anasayfaService.updateAnasayfa(this.anasayfa).subscribe(data => {

			var index=this.anasayfaList.findIndex(x=>x.anasayfaId==this.anasayfa.anasayfaId);
			this.anasayfaList[index]=this.anasayfa;
			this.dataSource = new MatTableDataSource(this.anasayfaList);
            this.configDataTable();
			this.anasayfa = new Anasayfa();
			jQuery('#anasayfa').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.anasayfaAddForm);

		})

	}

	createAnasayfaAddForm() {
		this.anasayfaAddForm = this.formBuilder.group({		
			anasayfaId : [0],
baslik : ["", Validators.required],
baslikYazi : ["", Validators.required],
aciklama : ["", Validators.required],
photo : ["", Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteAnasayfa(anasayfaId:number){
		this.anasayfaService.deleteAnasayfa(anasayfaId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.anasayfaList=this.anasayfaList.filter(x=> x.anasayfaId!=anasayfaId);
			this.dataSource = new MatTableDataSource(this.anasayfaList);
			this.configDataTable();
		})
	}

	getAnasayfaById(anasayfaId:number){
		this.clearFormGroup(this.anasayfaAddForm);
		this.anasayfaService.getAnasayfaById(anasayfaId).subscribe(data=>{
			this.anasayfa=data;
			this.anasayfaAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'anasayfaId')
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
