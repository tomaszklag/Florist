import { Component } from '@angular/core';
import { AppSandbox } from 'src/app/app-sandbox';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

  constructor (private sandbox: AppSandbox) {}

  onToolbarMenuToggle() {
    this.sandbox.toggleSidenav();
  }
}
