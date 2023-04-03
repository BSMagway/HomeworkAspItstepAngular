import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

@Injectable({providedIn: 'root'})
export class UserService {
    private jwt = '';

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    public signIn(username: string, password: string) {
      this.httpClient.post<any>("http://localhost:5201/" + 'api/user/login', { username, password })
            .subscribe(x => this.jwt = x.jwt);
    }

  public registration(username: string, password: string) {
    this.httpClient.post<any>("http://localhost:5201/" + 'api/user/register', { username, password }).subscribe();
  }

    public getJwt() {
        return this.jwt;
    }
}
