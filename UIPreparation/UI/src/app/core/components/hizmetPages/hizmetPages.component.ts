import { Component, OnInit,ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { HizmetDetail } from '../hizmetDetail/models/HizmetDetail';
import { HizmetDetailService } from '../hizmetDetail/services/HizmetDetail.service';
import { HizmetService } from '../hizmet/services/Hizmet.service';
import { Hizmet } from '../hizmet/models/Hizmet';

declare var jQuery: any;

@Component({
  selector: 'app-hizmetPages',
  templateUrl: './hizmetPages.component.html',
  styleUrls: ['./hizmetPages.component.scss']
})
export class HizmetPagesComponent implements OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['photo','editor',  'update', 'dil','delete','file'];

  hizmetDetails: HizmetDetail[] = [];
  hizmetDetailList:HizmetDetail[];
  hizmetDetail:HizmetDetail=new HizmetDetail();


  hizmmetDetailAddForm: FormGroup;
  photoForm: FormGroup;
  hizmet: Hizmet = new Hizmet();
  hizmetId: number;

  hizmetList:Hizmet[];

  modalBaslik: string = '';
  updateForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private hizmetService: HizmetService,
    private authService: AuthService,
     private hizmetDetailService:HizmetDetailService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, 
   
  ) {}

  ngAfterViewInit(): void {
    this.getHizmetDetailById(this.hizmet.hizmetId);

this.hizmetService.getHizmetList().subscribe(data=>this.hizmetList=data);

}

  ngOnInit() {
    this.getHizmetDetailById(this.hizmet.hizmetId);
    this.route.params.subscribe((params) => {
      const hizmetId = params['hizmetId'];
      this.hizmetId = +hizmetId;
      console.log('hizmetId:', this.hizmetId)
      this.getHizmetById(hizmetId);

      this.getHizmetDetailById(hizmetId);
    });
    this.createhizmetDetailAddForm();
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
		formData.append('hizmetDetailId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.hizmetDetailService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getHizmetDetailById(this.hizmet.hizmetId);
				console.log(data);
				this.alertifyService.success(data);
	})
	}

  getHizmetById(hizmetId: number) {
    this.hizmetService.getHizmetById(hizmetId).subscribe(
      (hizmet: Hizmet) => {
        this.hizmet = hizmet;
      },
      (error) => {
        // Hata yönetimi
      }
    );
  }
  
  getHizmetDetailById(hizmetId: number) {
    this.hizmetService.getHizmetDetailById(hizmetId).subscribe(
      (hizmetDetails: HizmetDetail[]) => {
        this.hizmetDetails = hizmetDetails;
        this.dataSource = new MatTableDataSource(hizmetDetails);
        this.configDataTable();
      },
      (error) => {
        console.error(error);
        this.hizmetDetails = []; 
      }
    );
  }



	getHizmetDetailList() {
		this.hizmetDetailService.getHizmetDetailList().subscribe(data => {
			this.hizmetDetailList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

  openModall() {
    this.hizmetDetail = new HizmetDetail();
    this.hizmmetDetailAddForm.patchValue({
      hizmetId: this.hizmet.hizmetId
    });
    jQuery('#hizmetdetail').modal('show');
  }

  save() {
    if (this.hizmmetDetailAddForm.valid) {
      this.hizmetDetail = { ...this.hizmmetDetailAddForm.value, hizmetId: this.hizmet.hizmetId };
  
      if (this.hizmetDetail.hizmetDetailId == 0)
        this.addHizmetDetail();
      else
        this.updateHizmetDetail();
    }
  }
  

	addHizmetDetail(){

		this.hizmetDetailService.addHizmetDetail(this.hizmetDetail).subscribe(data => {
      this.getHizmetDetailById(this.hizmet.hizmetId);
			this.hizmetDetail = new HizmetDetail();
			jQuery('#hizmetdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hizmmetDetailAddForm);

		})

	}

  updateHizmetDetail() {
    this.hizmetDetailService.updateHizmetDetail(this.hizmetDetail).subscribe(
      (data) => {
        const index = this.hizmetDetails.findIndex((x) => x.hizmetDetailId === this.hizmetDetail.hizmetDetailId);
        this.hizmetDetails[index] = { ...this.hizmetDetail }; 
        this.dataSource = new MatTableDataSource(this.hizmetDetails);
        this.configDataTable();
        this.hizmetDetail = new HizmetDetail();
        jQuery('#hizmetdetail').modal('hide');
        this.alertifyService.success(data);
        this.clearFormGroup(this.hizmmetDetailAddForm);
        this.getHizmetDetailById(this.hizmet.hizmetId);
      },
      
      (error) => {
        console.error('Hizmet detayı güncelleme hatası:', error);
      }
    );
  }
  

	createhizmetDetailAddForm() {
		this.hizmmetDetailAddForm = this.formBuilder.group({		
			hizmetDetailId : [0],
      hizmetId : [0],
      photoService : [""],
      photo : [""],
      editor : [""],
      dil : [0],
		})
	}

  deleteHizmetDetail(hizmetDetailId: number) {
    if (confirm('Hizmet detayını silmek istediğinize emin misiniz?')) {
      this.hizmetDetailService.deleteHizmetDetail(hizmetDetailId).subscribe(
        (data) => {
          this.alertifyService.success(data.toString());
          this.hizmetDetails = this.hizmetDetails.filter((x) => x.hizmetDetailId !== hizmetDetailId);
          this.dataSource = new MatTableDataSource(this.hizmetDetails);
          this.configDataTable();
        },
        (error) => {
          console.error('Hizmet detayı silme hatası:', error);
        }
      );
    }
  }
  ///Bura farklı
	getHizmetDetailiById(hizmetDetailId:number){
		this.clearFormGroup(this.hizmmetDetailAddForm);
		this.hizmetDetailService.getHizmetDetailiById(hizmetDetailId).subscribe(data=>{
			this.hizmetDetail=data;
			this.hizmmetDetailAddForm.patchValue(data);
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
