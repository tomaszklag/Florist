import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxsModule } from '@ngxs/store';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsState } from './dashboard/product-manager/store/products.state';
import { RouterModule } from '@angular/router';
import { SidenavModule } from './core/modules/sidenav/sidenav.module';
import { HeaderModule } from './core/modules/header/header.module';
import { AppState } from './store/app.state';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    NgxsModule.forRoot([AppState, ProductsState]),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    SidenavModule,
    HeaderModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
