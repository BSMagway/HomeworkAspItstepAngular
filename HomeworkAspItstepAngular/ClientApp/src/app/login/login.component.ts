import { Component } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  public login = '';
  public password = '';

  constructor(private userService: UserService) {}

  public signIn() {
    this.userService.signIn(this.login, this.password);
  }
}
