import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Anasayfa } from '../models/Anasayfa';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AnasayfaService {

  constructor(private httpClient: HttpClient) { }


  getAnasayfaList(): Observable<Anasayfa[]> {

    return this.httpClient.get<Anasayfa[]>(environment.getApiUrl + '/anasayfas/getall')
  }

  getAnasayfaById(id: number): Observable<Anasayfa> {
    return this.httpClient.get<Anasayfa>(environment.getApiUrl + '/anasayfas/getbyid?anasayfaId='+id)
  }

  addAnasayfa(anasayfa: Anasayfa): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/anasayfas/', anasayfa, { responseType: 'text' });
  }

  updateAnasayfa(anasayfa: Anasayfa): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/anasayfas/', anasayfa, { responseType: 'text' });

  }

  deleteAnasayfa(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/anasayfas/', { body: { anasayfaId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/anasayfas/addPhoto', formData, { responseType: 'text' });
  }
}