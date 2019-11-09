import { NgModule } from '@angular/core';
import { ProductManagerModule } from './product-manager/product-manager.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    DashboardRoutingModule,
    ProductManagerModule
  ],
  providers: [],
  bootstrap: [HomeComponent]
})
export class DashboardModule { }