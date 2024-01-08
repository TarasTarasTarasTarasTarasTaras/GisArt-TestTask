import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from 'src/environments/environment';
import { Trip } from "../models/trip";
import { Driver } from "../models/driver";

const baseUrl = `${environment.apiUrl}expedition/`;

@Injectable({
  providedIn: 'root'
})
export class ExpeditionService {
  constructor(private http: HttpClient) { }

  getAllTrips(params: HttpParams | null): Observable<Trip[]> {
    return this.http.get<Trip[]>(`${baseUrl}trips`, params === null ? {} : { params: params });
  }

  getAllDrivers(params: HttpParams | null): Observable<Driver[]> {
    return this.http.get<Driver[]>(`${baseUrl}drivers`, params === null ? {} : { params: params });
  }
}