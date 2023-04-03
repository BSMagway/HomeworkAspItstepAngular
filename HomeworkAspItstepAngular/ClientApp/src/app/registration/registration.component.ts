import { Component } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-registration-component',
  templateUrl: './registration.component.html'
})
export class RegistrationComponent {
  public login = '';
  public password = '';

  constructor(private userService: UserService) {}

  public registration() {
    this.userService.registration(this.login, this.password);
  }
}
