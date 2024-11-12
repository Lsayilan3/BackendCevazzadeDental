import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Durum } from '../models/Durum';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DurumService {

  constructor(private httpClient: HttpClient) { }


  getDurumList(): Observable<Durum[]> {

    return this.httpClient.get<Durum[]>(environment.getApiUrl + '/durums/getall')
  }

  getDurumById(id: number): Observable<Durum> {
    return this.httpClient.get<Durum>(environment.getApiUrl + '/durums/getbyid?durumId='+id)
  }

  addDurum(durum: Durum): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/durums/', durum, { responseType: 'text' });
  }

  updateDurum(durum: Durum): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/durums/', durum, { responseType: 'text' });

  }

  deleteDurum(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/durums/', { body: { durumId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/durums/addPhoto', formData, { responseType: 'text' });
  }
}