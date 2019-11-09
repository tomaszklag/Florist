import { NgModule } from '@angular/core';
import { ProductManagerComponent } from './components/product-manager.component';
import { NgxsModule } from '@ngxs/store';
import { ProductsState } from './store/products.state';

@NgModule({
  declarations: [
    ProductManagerComponent
  ],
  imports: [
    NgxsModule.forRoot([ProductsState])
  ],
  providers: [],
  bootstrap: [ProductManagerComponent]
})
export class ProductManagerModule { }
