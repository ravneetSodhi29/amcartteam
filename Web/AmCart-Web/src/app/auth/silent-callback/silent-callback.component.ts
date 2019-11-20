import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-silent-callback',
  templateUrl: './silent-callback.component.html',
  styleUrls: ['./silent-callback.component.css']
})
export class SilentCallbackComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.silentCallback().then(user => {
      let returnUrl = sessionStorage.getItem('returnUrl') || '';
      this.router.navigate([returnUrl]);
    }, error => {
        console.error(error);
    });
  }

}
