import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { DialogModule } from 'primeng/dialog';
import { PasswordModule } from 'primeng/password';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { InputTextModule } from 'primeng/inputtext';
import { RegisterData } from '../../models/account/registerData';
import { Button, ButtonModule } from 'primeng/button';
import { LoginData } from '../../models/account/loginData';
import { SelectButtonModule } from 'primeng/selectbutton';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { Favorite } from '../../models/navbar/favorite';
import { InfoService } from '../../services/info.service';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, DialogModule, PasswordModule, FormsModule, ReactiveFormsModule, InputTextModule, ButtonModule, SelectButtonModule, OverlayPanelModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
  providers: []
})
export class NavbarComponent {

  userType : string = ""
  avatar: string = "/assets/img/navbar/avatar.png"
  visible : boolean = false
  connectionChoice : string = ""
  loading: boolean = false
  stateOptions: any[] = [
    { label: 'Client', value: 'Customer' },
    { label: 'Propriétaire', value: 'Owner' }
]
  value : string = ""
  loginForm : FormGroup
  registerForm : FormGroup
  filterForm : FormGroup

  searchValue : string = ""

  constructor(private _authService : AuthService, private _apiService : ApiService, private _fb : FormBuilder, private _router : Router, private _infoService : InfoService){
    this.loginForm = this._fb.group({
      humanIdentifier : [null, [Validators.required]],
      humanPassword : [null, [Validators.required]]
    })
    this.registerForm = this._fb.group({
      humanLastName :[null, [Validators.required]],
      humanFirstName :[null, [Validators.required]],
      humanMail :[null, [Validators.required]],
      humanPassword :[null, [Validators.required]],
      humanPhoneNumber :[null, [Validators.required]],
      humanType :[null, [Validators.required]],
    })
    this.filterForm = this._fb.group({
      filter : []
    })
  }

  ngOnInit() : void {
    const userConnected = this._authService.getType()
    userConnected ? this.userType = userConnected : this.userType = ""
    this._infoService.setFavorites()
  }

  getFavorites() : Favorite[] {    
    return this._infoService.getFavorites()
  }

  login() : void {
    if(this.loginForm.valid){
      const user : LoginData = {
        humanIdentifier : this.loginForm.get("humanIdentifier")?.value,
        humanPassword : this.loginForm.get("humanPassword")?.value
      }
      this._apiService.login(user).subscribe({
        next : (resp) => {
          console.log(resp)
          this._authService.setUser(resp.user.humanId, resp.user.humanType, resp.token)
          this.userType = resp.user.humanType
          this._infoService.getReservations()
          this._infoService.getFavorites()
          this.visible = false
        },
        error : (error) => console.log(error)
      })
    }
  }

  register() : void {
    if(this.registerForm.valid){
      const newUser : RegisterData = {
        humanLastName : this.registerForm.get("humanLastName")?.value,
        humanFirstName : this.registerForm.get("humanFirstName")?.value,
        humanMail : this.registerForm.get("humanMail")?.value,
        humanPassword : this.registerForm.get("humanPassword")?.value,
        humanPhoneNumber : this.registerForm.get("humanPhoneNumber")?.value,
        humanType : this.registerForm.get("humanType")?.value
      }
      this._apiService.register(newUser).subscribe({
        next : (resp) => {
          console.log(resp)
          this.visible = false
        },
        error : (error) => console.log(error)
      })
    }
  }

  logout() : void {
    this._authService.logout()
    this.userType = ""
  }

  showDialog(choice : string) :void {
    this.connectionChoice = choice
    this.visible = true;
  }
  
  load() {
    this.loading = true;

    setTimeout(() => {
        this.loading = false
    }, 2000);
  }

  searchRestaurant(name : string) {
    console.log("test");
    
    this._apiService.getRestaurantsByIdentifier(name).subscribe({
      next : resp => this._router.navigateByUrl("restaurant/" + resp[0].restaurantId)
    })
  }
  
}
