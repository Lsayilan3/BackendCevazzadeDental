import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Blog } from './models/Blog';
import { BlogService } from './services/Blog.service';
import { environment } from 'environments/environment';
import { Router } from '@angular/router';

declare var jQuery: any;

@Component({
	selector: 'app-blog',
	templateUrl: './blog.component.html',
	styleUrls: ['./blog.component.scss']
})
export class BlogComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['blogId','photo','aciklayan','tarih','baslik','aciklama','dil', 'update','delete','file','search'];

	blogList:Blog[];
	blog:Blog=new Blog();

	blogAddForm: FormGroup;
	photoForm: FormGroup;


	blogId:number;

	constructor(private router: Router,private blogService:BlogService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getBlogList();
    }

	ngOnInit() {

		this.createBlogAddForm();
	}

	navigateToRotaPages(blogId: number) {
		this.router.navigate(['/blogpages', blogId]);
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
		formData.append('blogId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.blogService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getBlogList();
				console.log(data);
				this.alertifyService.success(data);
	})
	}


	getBlogList() {
		this.blogService.getBlogList().subscribe(data => {
			this.blogList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.blogAddForm.valid) {
			this.blog = Object.assign({}, this.blogAddForm.value)

			if (this.blog.blogId == 0)
				this.addBlog();
			else
				this.updateBlog();
		}

	}

	addBlog(){

		this.blogService.addBlog(this.blog).subscribe(data => {
			this.getBlogList();
			this.blog = new Blog();
			jQuery('#blog').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.blogAddForm);

		})

	}

	updateBlog(){

		this.blogService.updateBlog(this.blog).subscribe(data => {

			var index=this.blogList.findIndex(x=>x.blogId==this.blog.blogId);
			this.blogList[index]=this.blog;
			this.dataSource = new MatTableDataSource(this.blogList);
            this.configDataTable();
			this.blog = new Blog();
			jQuery('#blog').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.blogAddForm);

		})

	}

	createBlogAddForm() {
		this.blogAddForm = this.formBuilder.group({		
			blogId : [0],
photo : ["", Validators.required],
aciklayan : ["", Validators.required],
tarih : ["", Validators.required],
baslik : ["", Validators.required],
aciklama : ["", Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteBlog(blogId:number){
		this.blogService.deleteBlog(blogId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.blogList=this.blogList.filter(x=> x.blogId!=blogId);
			this.dataSource = new MatTableDataSource(this.blogList);
			this.configDataTable();
		})
	}

	getBlogById(blogId:number){
		this.clearFormGroup(this.blogAddForm);
		this.blogService.getBlogById(blogId).subscribe(data=>{
			this.blog=data;
			this.blogAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'blogId')
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
