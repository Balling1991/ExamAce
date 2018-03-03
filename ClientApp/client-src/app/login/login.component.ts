import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from '../shared/loginService';

@Component({
	selector: 'login-component',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.css']
})
export class LoginComponent {

	return: string = "";
	errorMessage: string = ""

	public creds = {
		username: "",
		password: ""
	}

	constructor(
		private loginService: LoginService,
		private router: Router,
		private route: ActivatedRoute){}

	
	ngOnInit() {
		this.route.queryParams
			.subscribe(params => this.return = params['return'] || '');
	}

	login() {
		if(this.loginService.isValid()) {
			return;
		} else {
		this.loginService.login(this.creds)
			.subscribe(success => {
				if(success) {
					this.router.navigate([""]);
				}
			}, err => this.errorMessage = "Failed to login");
		}
	}
}
