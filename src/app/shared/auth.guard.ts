import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService:AuthService, private router:Router){}
  canActivate() {
    if(this.authService.isLoggedIn()){
      return true;
    }else{
      alert("Please Login first to continue")
      this.router.navigate(['login'])
      return false;
    }
    
  }
  
}
