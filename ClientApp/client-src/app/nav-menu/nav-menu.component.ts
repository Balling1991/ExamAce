import { Component } from '@angular/core';
import { LoginService } from '../shared/loginService';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isExpanded = false;

  constructor(private loginService: LoginService){}

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  isLoggedIn() {
    return this.loginService.isValid();
  }
  
	logout() {
		this.loginService.logout();
    }
}
