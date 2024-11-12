import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogDetail } from '../models/BlogDetail';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BlogDetailService {

  constructor(private httpClient: HttpClient) { }


  getBlogDetailList(): Observable<BlogDetail[]> {

    return this.httpClient.get<BlogDetail[]>(environment.getApiUrl + '/blogDetails/getall')
  }
///Bura farklı
  getBlogDetailiById(id: number): Observable<BlogDetail> {
    return this.httpClient.get<BlogDetail>(environment.getApiUrl + '/blogDetails/getbyid?blogDetailId='+id)
  }

  addBlogDetail(blogDetail: BlogDetail): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/blogDetails/', blogDetail, { responseType: 'text' });
  }

  getBlogDetailByBlogId(blogId: any): Observable<any> {
    return this.httpClient.get(environment.getApiUrl + `/blogDetails/getlist?blogId=`+ blogId);
  }

  updateBlogDetail(blogDetail: BlogDetail): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/blogDetails/', blogDetail, { responseType: 'text' });

  }

  deleteBlogDetail(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/blogDetails/', { body: { blogDetailId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/blogDetails/addPhoto', formData, { responseType: 'text' });
  }

}