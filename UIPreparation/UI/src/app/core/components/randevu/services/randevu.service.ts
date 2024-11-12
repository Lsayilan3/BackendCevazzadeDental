import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Randevu } from '../models/Randevu';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RandevuService {

  constructor(private httpClient: HttpClient) { }


  getRandevuList(): Observable<Randevu[]> {

    return this.httpClient.get<Randevu[]>(environment.getApiUrl + '/randevus/getall')
  }

  getRandevuById(id: number): Observable<Randevu> {
    return this.httpClient.get<Randevu>(environment.getApiUrl + '/randevus/getbyid?randevuId='+id)
  }

  addRandevu(randevu: Randevu): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/randevus/', randevu, { responseType: 'text' });
  }

  updateRandevu(randevu: Randevu): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/randevus/', randevu, { responseType: 'text' });

  }

  deleteRandevu(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/randevus/', { body: { randevuId: id } });
  }


}