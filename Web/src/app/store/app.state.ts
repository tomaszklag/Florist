import { State, Action, StateContext, Selector } from '@ngxs/store';
import { ToggleSidenav } from './sidenav.actions';
import { ProductsState } from 'src/app/dashboard/product-manager/store/products.state';
import { AppStateModel } from '../core/models/states/app.state.model';

@State<AppStateModel>({
  name: 'appstate',
  defaults: {
    isSidnavExpanded: false
  },
  children: [ProductsState]
})
export class AppState { 

  @Selector()
  static isSidnavExpanded(state: AppStateModel): boolean {
    return state.isSidnavExpanded;
  }

  @Action(ToggleSidenav)
  toggleSidenav(ctx: StateContext<AppStateModel>) {
    const state = ctx.getState();
    ctx.patchState({
      isSidnavExpanded: !state.isSidnavExpanded
    })
  }
}