import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';
import { UserService } from '../../shared/services/user.service';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [RouterOutlet, RouterLink,],
  templateUrl: './main-layout.component.html',
  styles: ``
})
export class MainLayoutComponent implements OnInit {
  constructor(private router: Router, private authService:AuthService, private userService:UserService) { }
  fullName: string = ''

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe({
      next: (res: any) => this.fullName = res.fullName,
      error: (err: any) => console.log('error while retrieving user profile:\n', err)
    })
  }

  onLogout() {
    this.authService.deleteToken();
    this.router.navigateByUrl('/signin');
  }
}
