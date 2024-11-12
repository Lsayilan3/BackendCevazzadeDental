import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HekimDetail } from '../models/HekimDetail';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class HekimDetailService {

  constructor(private httpClient: HttpClient) { }


  getHekimDetailList(): Observable<HekimDetail[]> {

    return this.httpClient.get<HekimDetail[]>(environment.getApiUrl + '/hekimDetails/getall')
  }
///Bura farklı
  getHekimDetailiById(id: number): Observable<HekimDetail> {
    return this.httpClient.get<HekimDetail>(environment.getApiUrl + '/hekimDetails/getbyid?hekimDetailId='+id)
  }

  addHekimDetail(hekimDetail: HekimDetail): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/hekimDetails/', hekimDetail, { responseType: 'text' });
  }

  getHekimDetailByHekimId(hekimId: any): Observable<any> {
    return this.httpClient.get(environment.getApiUrl + `/hekimDetails/getlist?hekimId=`+ hekimId);
  }
  
  updateHekimDetail(hekimDetail: HekimDetail): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/hekimDetails/', hekimDetail, { responseType: 'text' });

  }

  deleteHekimDetail(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/hekimDetails/', { body: { hekimDetailId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/hekimDetails/addPhoto', formData, { responseType: 'text' });
  }
}