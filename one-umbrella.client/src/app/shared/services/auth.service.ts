import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

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
  
  getUser() : string | null {
    return localStorage.getItem('userId')
  }
  
  getType() : string | null {
    return localStorage.getItem('type')
  }

  logout() : void {
    localStorage.clear()
    this._router.navigateByUrl("/")
    
  }
}
