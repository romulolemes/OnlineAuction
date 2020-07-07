import { Component, OnInit } from '@angular/core';
import { AuthService } from './service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(public authService: AuthService) {}

  ngOnInit(): void {
    this.authService
      .getUser()
      .then((user) => (this.userName = user?.profile.name));
  }

  public userName: string;

  public onLogout() {
    this.authService.logout();
  }
}
