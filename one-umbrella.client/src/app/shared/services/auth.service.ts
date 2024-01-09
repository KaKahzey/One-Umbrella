import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _router : Router){}
  
  getToken(): string | null {
    return localStorage.getItem('token')
  }

  setUser(id : number, type : string, token : string) : void {
    localStorage.setItem('userId', id.toString())
    localStorage.setItem('type', type.toString())
    localStorage.setItem('token', token.toString())
  }
  
  getUser() : string | null {
    return localStorage.getItem('userId')
  }

  logout() : void {
    localStorage.clear()
    this._router.navigateByUrl("/")
    
  }
}
