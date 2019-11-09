import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Store, Select } from '@ngxs/store';
import { ToggleSidenav } from './store/sidenav.actions';
import { AppState } from './store/app.state';

@Injectable()
export class AppSandbox {

  constructor(
    private store: Store,
  ) { }

  @Select(AppState.isSidnavExpanded) isSidnavExpanded$: Observable<boolean>;

  toggleSidenav(): void {
    this.store.dispatch(new ToggleSidenav());
  }
}