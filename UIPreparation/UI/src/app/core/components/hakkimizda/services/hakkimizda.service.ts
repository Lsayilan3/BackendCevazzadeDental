import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hakkimizda } from '../models/Hakkimizda';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class HakkimizdaService {

  constructor(private httpClient: HttpClient) { }


  getHakkimizdaList(): Observable<Hakkimizda[]> {

    return this.httpClient.get<Hakkimizda[]>(environment.getApiUrl + '/hakkimizdas/getall')
  }

  getHakkimizdaById(id: number): Observable<Hakkimizda> {
    return this.httpClient.get<Hakkimizda>(environment.getApiUrl + '/hakkimizdas/getbyid?hakkimizdaId='+id)
  }

  addHakkimizda(hakkimizda: Hakkimizda): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/hakkimizdas/', hakkimizda, { responseType: 'text' });
  }

  updateHakkimizda(hakkimizda: Hakkimizda): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/hakkimizdas/', hakkimizda, { responseType: 'text' });

  }

  deleteHakkimizda(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/hakkimizdas/', { body: { hakkimizdaId: id } });
  }
  
  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/hakkimizdas/addPhoto', formData, { responseType: 'text' });
  }
}