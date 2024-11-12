import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hizmet } from '../models/Hizmet';
import { environment } from 'environments/environment';
import { HizmetDetail } from '../../hizmetDetail/models/HizmetDetail';


@Injectable({
  providedIn: 'root'
})
export class HizmetService {

  constructor(private httpClient: HttpClient) { }


  getHizmetList(): Observable<Hizmet[]> {

    return this.httpClient.get<Hizmet[]>(environment.getApiUrl + '/hizmets/getall')
  }

  getHizmetById(id: number): Observable<Hizmet> {
    return this.httpClient.get<Hizmet>(environment.getApiUrl + '/hizmets/getbyid?hizmetId='+id)
  }

  getHizmetDetailById(id: number): Observable<HizmetDetail[]> {
    return this.httpClient.get<HizmetDetail[]>(environment.getApiUrl + '/hizmetDetails/getlist?hizmetId=' + id);
  }

  addHizmet(hizmet: Hizmet): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/hizmets/', hizmet, { responseType: 'text' });
  }

  updateHizmet(hizmet: Hizmet): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/hizmets/', hizmet, { responseType: 'text' });

  }

  deleteHizmet(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/hizmets/', { body: { hizmetId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/hizmets/addPhoto', formData, { responseType: 'text' });
  }
}