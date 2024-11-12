import { Component, OnInit,ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { HekimDetail } from '../hekimDetail/models/HekimDetail';
import { Hekim } from '../hekim/models/Hekim';
import { ActivatedRoute, Router } from '@angular/router';
import { HekimService } from '../hekim/services/Hekim.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { HekimDetailService } from '../hekimDetail/services/HekimDetail.service';

declare var jQuery: any;

@Component({
  selector: 'app-hekimPages',
  templateUrl: './hekimPages.component.html',
  styleUrls: ['./hekimPages.component.scss']
})
export class HekimPagesComponent implements OnInit {
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['photo','ad','uzmanlik','aciklama','sosyalFace','sosyalTwitter',
    'sosyalingstagram','sosyalMail', 'dil','update','delete','file'];

  hekimDetails: HekimDetail[] = [];
  hekimDetailList:HekimDetail[];
  hekimDetail:HekimDetail=new HekimDetail();


  hekimDetailAddForm: FormGroup;
  photoForm: FormGroup;
  hekim: Hekim = new Hekim();
  hekimId: number;

  hekimList:Hekim[];

  modalBaslik: string = '';
  updateForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private hekimService: HekimService,
    private authService: AuthService,
     private hekimDetailService:HekimDetailService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, 
   
  ) {}

  ngAfterViewInit(): void {
    this.getHekimDetailById(this.hekim.hekimId);

this.hekimService.getHekimList().subscribe(data=>this.hekimList=data);

}

  ngOnInit() {
    this.getHekimDetailById(this.hekim.hekimId);
    this.route.params.subscribe((params) => {
      const hekimId = params['hekimId'];
      this.getHekimById(hekimId);
      this.getHekimDetailById(hekimId);
    });
    this.createhekimDetailAddForm();
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
		formData.append('hekimDetailId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.hekimDetailService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getHekimDetailById(this.hekim.hekimId);
				console.log(data);
				this.alertifyService.success(data);
	})
	}

 


  getHekimById(hekimId: number) {
    this.hekimService.getHekimById(hekimId).subscribe(
      (hekim: Hekim) => {
        this.hekim = hekim;
      },
      (error) => {
        // Hata yönetimi
      }
    );
  }
  
  getHekimDetailById(hekimId: number) {
    this.hekimService.getHekimDetailById(hekimId).subscribe(
      (hekimDetails: HekimDetail[]) => {
        this.hekimDetails = hekimDetails;
        this.dataSource = new MatTableDataSource(hekimDetails);
        this.configDataTable();
      },
      (error) => {
        console.error(error);
        this.hekimDetails = []; 
      }
    );
  }



	getHekimDetailList() {
		this.hekimDetailService.getHekimDetailList().subscribe(data => {
			this.hekimDetailList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

  openModall() {
    this.hekimDetail = new HekimDetail();
    this.hekimDetailAddForm.patchValue({
      hekimId: this.hekim.hekimId
    });
    jQuery('#hekimdetail').modal('show');
  }

  save() {
    if (this.hekimDetailAddForm.valid) {
      this.hekimDetail = { ...this.hekimDetailAddForm.value, hekimId: this.hekim.hekimId };
  
      if (this.hekimDetail.hekimDetailId == 0)
        this.addHekimDetail();
      else
        this.updateHekimDetail();
    }
  }
  

	addHekimDetail(){

		this.hekimDetailService.addHekimDetail(this.hekimDetail).subscribe(data => {
      this.getHekimDetailById(this.hekim.hekimId);
			this.hekimDetail = new HekimDetail();
			jQuery('#hekimdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hekimDetailAddForm);

		})

	}

  updateHekimDetail() {
    this.hekimDetailService.updateHekimDetail(this.hekimDetail).subscribe(
      (data) => {
        const index = this.hekimDetails.findIndex((x) => x.hekimDetailId === this.hekimDetail.hekimDetailId);
        this.hekimDetails[index] = { ...this.hekimDetail }; // Güncellenen hekimDetail nesnesini dizideki ilgili örnekle değiştirin
        this.dataSource = new MatTableDataSource(this.hekimDetails);
        this.configDataTable();
        this.hekimDetail = new HekimDetail();
        jQuery('#hekimdetail').modal('hide');
        this.alertifyService.success(data);
        this.clearFormGroup(this.hekimDetailAddForm);
        this.getHekimDetailById(this.hekim.hekimId);
      },
      
      (error) => {
        console.error('Rota detayı güncelleme hatası:', error);
      }
    );
  }
  

	createhekimDetailAddForm() {
		this.hekimDetailAddForm = this.formBuilder.group({		
			hekimDetailId : [0],
hekimId : [0],
photo : [""],
ad : [""],
uzmanlik : [""],
aciklama : [""],
sosyalFace : [""],
sosyalTwitter : [""],
sosyalingstagram : [""],
sosyalMail : [""],
dil : [0]
		})
	}

  deleteHekimDetail(hekimDetailId: number) {
    if (confirm('Hekim detayını silmek istediğinize emin misiniz?')) {
      this.hekimDetailService.deleteHekimDetail(hekimDetailId).subscribe(
        (data) => {
          this.alertifyService.success(data.toString());
          this.hekimDetails = this.hekimDetails.filter((x) => x.hekimDetailId !== hekimDetailId);
          this.dataSource = new MatTableDataSource(this.hekimDetails);
          this.configDataTable();
        },
        (error) => {
          console.error('Hekim detayı silme hatası:', error);
        }
      );
    }
  }
  ///Bura farklı
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


