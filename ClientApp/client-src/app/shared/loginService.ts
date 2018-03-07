import { HttpClient } from "@angular/common/http";
import { Response } from "@angular/http"
import { Injectable } from "@angular/core";

@Injectable()
export class LoginService {

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
                this.createSession(tokenInfo.username, tokenInfo.token, tokenInfo.tokenExpiration);
                return true;
            });
    }

    private createSession(username, token, tokenExpiration) {
        localStorage.setItem('userName', username);
        localStorage.setItem('id_token', token);
        localStorage.setItem('expires_at', JSON.stringify(tokenExpiration.valueOf()));
	}

    logout() {
        localStorage.removeItem("userName");
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_at");
        this.isValid();
    }
}
