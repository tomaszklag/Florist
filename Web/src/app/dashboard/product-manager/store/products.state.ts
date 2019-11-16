import { State } from '@ngxs/store';

@State<any>({
  name: 'products',
  children: []
})
export class ProductsState { }