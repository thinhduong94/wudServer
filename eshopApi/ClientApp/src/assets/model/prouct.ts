export class productView {
  id: number;
  name: string;
  price: number;
  imgName: string;
  category_id: number;
  band_id: number;
  productChilViews: Array<productChilView>;
  category: categoryView;
  band: bandView;
}
export class categoryView {
  id: number;
  name: string;
}
export class bandView {
  id: number;
  name: string;
}
export class productChilView {
  id: number;
  product_id: number;
  sizeName: string;
  sizeValue: string;
  colorName: string;
  colorValue: string;
}
