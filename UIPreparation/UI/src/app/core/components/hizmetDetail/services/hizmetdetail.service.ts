import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HizmetDetail } from '../models/HizmetDetail';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class HizmetDetailService {

  constructor(private httpClient: HttpClient) { }


  getHizmetDetailList(): Observable<HizmetDetail[]> {

    return this.httpClient.get<HizmetDetail[]>(environment.getApiUrl + '/hizmetDetails/getall')
  }
///Bura farklı
  getHizmetDetailiById(id: number): Observable<HizmetDetail> {
    return this.httpClient.get<HizmetDetail>(environment.getApiUrl + '/hizmetDetails/getbyid?hizmetDetailId='+id)
  }

  addHizmetDetail(hizmetDetail: HizmetDetail): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/hizmetDetails/', hizmetDetail, { responseType: 'text' });
  }

  getHizmetDetailByHekimId(hizmetId: any): Observable<any> {
    return this.httpClient.get(environment.getApiUrl + `/hizmetDetails/getlist?hizmetId=`+ hizmetId);
  }
  
  updateHizmetDetail(hizmetDetail: HizmetDetail): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/hizmetDetails/', hizmetDetail, { responseType: 'text' });

  }

  deleteHizmetDetail(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/hizmetDetails/', { body: { hizmetDetailId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/hizmetDetails/addPhoto', formData, { responseType: 'text' });
  }
}