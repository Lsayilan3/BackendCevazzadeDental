import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hekim } from '../models/Hekim';
import { environment } from 'environments/environment';
import { HekimDetail } from '../../hekimDetail/models/HekimDetail';


@Injectable({
  providedIn: 'root'
})
export class HekimService {

  constructor(private httpClient: HttpClient) { }


  getHekimList(): Observable<Hekim[]> {

    return this.httpClient.get<Hekim[]>(environment.getApiUrl + '/hekims/getall')
  }

  getHekimById(id: number): Observable<Hekim> {
    return this.httpClient.get<Hekim>(environment.getApiUrl + '/hekims/getbyid?hekimId='+id)
  }

  getHekimDetailById(id: number): Observable<HekimDetail[]> {
    return this.httpClient.get<HekimDetail[]>(environment.getApiUrl + '/hekimDetails/getlist?hekimId=' + id);
  }

  addHekim(hekim: Hekim): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/hekims/', hekim, { responseType: 'text' });
  }

  updateHekim(hekim: Hekim): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/hekims/', hekim, { responseType: 'text' });

  }

  deleteHekim(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/hekims/', { body: { hekimId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/hekims/addPhoto', formData, { responseType: 'text' });
  }
}