import { HttpClient } from "@angular/common/http";
import { Response } from "@angular/http"
import { Injectable } from "@angular/core";

@Injectable()
export class LoginService {

    private username: string = "";
    private token: string = "";
    private tokenExpiration: Date;


    constructor(private http: HttpClient){}

    isValid(): boolean {    
        const tokenExp = localStorage.getItem('expires_at');
        var expiresAt = tokenExp.substring(1, tokenExp.length - 1);

        if(expiresAt == null) {
            return false
        }

        let expireDate = new Date(expiresAt);
        let rightNow = new Date();
        return !(expireDate < rightNow);
    }

    login(creds) {
        return this.http.post("/account/createtoken", creds)
            .map((result: any) => {
                let tokenInfo = result;
                this.username = tokenInfo.username
                this.token = tokenInfo.token;
                this.tokenExpiration = tokenInfo.expiration;
                this.createSession(this.username, this.token, this.tokenExpiration);
                return true;
            });
    }

    private createSession(username, token, tokenExpiration) {
        localStorage.setItem('userName', this.username);
        localStorage.setItem('id_token', this.token);
        localStorage.setItem('expires_at', JSON.stringify(this.tokenExpiration.valueOf()));

	}

    logout() {
        localStorage.removeItem("userName");
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_at");
        this.isValid();
    }
}
