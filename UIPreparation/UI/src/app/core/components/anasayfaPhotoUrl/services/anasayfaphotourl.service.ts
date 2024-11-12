import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AnasayfaPhotoUrl } from '../models/AnasayfaPhotoUrl';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AnasayfaPhotoUrlService {

  constructor(private httpClient: HttpClient) { }


  getAnasayfaPhotoUrlList(): Observable<AnasayfaPhotoUrl[]> {

    return this.httpClient.get<AnasayfaPhotoUrl[]>(environment.getApiUrl + '/anasayfaPhotoUrls/getall')
  }

  getAnasayfaPhotoUrlById(id: number): Observable<AnasayfaPhotoUrl> {
    return this.httpClient.get<AnasayfaPhotoUrl>(environment.getApiUrl + '/anasayfaPhotoUrls/getbyid?anasayfaPhotoUrlId='+id)
  }

  addAnasayfaPhotoUrl(anasayfaPhotoUrl: AnasayfaPhotoUrl): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/anasayfaPhotoUrls/', anasayfaPhotoUrl, { responseType: 'text' });
  }

  updateAnasayfaPhotoUrl(anasayfaPhotoUrl: AnasayfaPhotoUrl): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/anasayfaPhotoUrls/', anasayfaPhotoUrl, { responseType: 'text' });

  }

  deleteAnasayfaPhotoUrl(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/anasayfaPhotoUrls/', { body: { anasayfaPhotoUrlId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/anasayfaPhotoUrls/addPhoto', formData, { responseType: 'text' });
  }
}