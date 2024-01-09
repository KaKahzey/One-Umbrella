import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  user: any = {
    name: "Sébastien",
    type: "Owner"
  }

  avatar: string = "/assets/img/navbar/avatar.png"
}
