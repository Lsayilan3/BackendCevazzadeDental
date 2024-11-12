import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Randevu } from './models/Randevu';
import { RandevuService } from './services/Randevu.service';
import { environment } from 'environments/environment';
import { HizmetService } from '../hizmet/services/Hizmet.service';
import { Hizmet } from '../hizmet/models/Hizmet';

declare var jQuery: any;

@Component({
	selector: 'app-randevu',
	templateUrl: './randevu.component.html',
	styleUrls: ['./randevu.component.scss']
})
export class RandevuComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['randevuId','ad','soyad','tel','hizmetlerimizId','tarih','craeteDate','mesaj', 'update','delete'];

	randevuList:Randevu[];
	randevu:Randevu=new Randevu();

	randevuAddForm: FormGroup;

	hizmetList:Hizmet[];
	randevuId:number;

	constructor(private hizmetService: HizmetService, private randevuService:RandevuService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
		this.hizmetService.getHizmetList().subscribe(data=>this.hizmetList=data);
        this.getRandevuList();
    }

	ngOnInit() {
		this.hizmetService.getHizmetList().subscribe(data=>this.hizmetList=data);
		this.createRandevuAddForm();
	}


	getRandevuList() {
		this.randevuService.getRandevuList().subscribe(data => {
			this.randevuList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.randevuAddForm.valid) {
			this.randevu = Object.assign({}, this.randevuAddForm.value)

			if (this.randevu.randevuId == 0)
				this.addRandevu();
			else
				this.updateRandevu();
		}

	}

	addRandevu(){

		this.randevuService.addRandevu(this.randevu).subscribe(data => {
			this.getRandevuList();
			this.randevu = new Randevu();
			jQuery('#randevu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.randevuAddForm);

		})

	}

	updateRandevu(){

		this.randevuService.updateRandevu(this.randevu).subscribe(data => {

			var index=this.randevuList.findIndex(x=>x.randevuId==this.randevu.randevuId);
			this.randevuList[index]=this.randevu;
			this.dataSource = new MatTableDataSource(this.randevuList);
            this.configDataTable();
			this.randevu = new Randevu();
			jQuery('#randevu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.randevuAddForm);

		})

	}

	createRandevuAddForm() {
		this.randevuAddForm = this.formBuilder.group({		
			randevuId : [0],
ad : ["", Validators.required],
soyad : ["", Validators.required],
tel : [0, Validators.required],
hizmetlerimizId : [0, Validators.required],
tarih : [null, Validators.required],
craeteDate : [null, Validators.required],
mesaj : ["", Validators.required]
		})
	}

	deleteRandevu(randevuId:number){
		this.randevuService.deleteRandevu(randevuId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.randevuList=this.randevuList.filter(x=> x.randevuId!=randevuId);
			this.dataSource = new MatTableDataSource(this.randevuList);
			this.configDataTable();
		})
	}

	getRandevuById(randevuId:number){
		this.clearFormGroup(this.randevuAddForm);
		this.randevuService.getRandevuById(randevuId).subscribe(data=>{
			this.randevu=data;
			this.randevuAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'randevuId')
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
