import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { API_URL } from "../constants/URL";
import { User } from "../models/User";

@Injectable({
    providedIn: 'root',
  })
export class AdminService {
    constructor(private readonly http: HttpClient) {}
  
    getUsers() {
        return this.http.get<User[]>(`${API_URL}/admin`);
    }
}