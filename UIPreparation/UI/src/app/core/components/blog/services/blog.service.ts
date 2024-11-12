import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Blog } from '../models/Blog';
import { environment } from 'environments/environment';
import { BlogDetail } from '../../blogDetail/models/BlogDetail';


@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private httpClient: HttpClient) { }


  getBlogList(): Observable<Blog[]> {

    return this.httpClient.get<Blog[]>(environment.getApiUrl + '/blogs/getall')
  }

  getBlogById(id: number): Observable<Blog> {
    return this.httpClient.get<Blog>(environment.getApiUrl + '/blogs/getbyid?blogId='+id)
  }

  getBlogDetailById(id: number): Observable<BlogDetail[]> {
    return this.httpClient.get<BlogDetail[]>(environment.getApiUrl + '/blogDetails/getlist?blogId=' + id);
  }

  addBlog(blog: Blog): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/blogs/', blog, { responseType: 'text' });
  }

  updateBlog(blog: Blog): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/blogs/', blog, { responseType: 'text' });

  }

  deleteBlog(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/blogs/', { body: { blogId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/blogs/addPhoto', formData, { responseType: 'text' });
  }

}