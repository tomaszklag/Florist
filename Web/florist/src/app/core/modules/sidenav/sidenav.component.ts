import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { AppSandbox } from 'src/app/app-sandbox';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent {

  isExpanded: Observable<boolean>;
  
  constructor (private sandbox: AppSandbox) {
    this.isExpanded = this.sandbox.isSidnavExpanded$;
  }
}
