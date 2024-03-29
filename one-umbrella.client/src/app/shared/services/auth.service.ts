import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { InfoService } from './info.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  constructor(private _router : Router){}
  
  setUser(id : number, type : string, token : string) : void {
    localStorage.setItem('userId', id.toString())
    localStorage.setItem('type', type.toString())
    localStorage.setItem('token', token.toString())
  }

  getToken(): string | null {
    return localStorage.getItem('token')
  }
  
  getUser() : number | null {
    const userString = localStorage.getItem('userId')
    return userString ? parseInt(userString) : null
  }
  
  getType() : string | null {
    return localStorage.getItem('type')
  }

  logout() : void {
    localStorage.clear()
    this._router.navigateByUrl("/")
  }
}
