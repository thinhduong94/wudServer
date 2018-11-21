import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { productView } from '../../assets/model/prouct';

@Injectable()
export class ProductService {
  baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }
  getAll() {
    return this.http.get<productView[]>(this.baseUrl + 'api/product');
  }
  create(product) {
    var data = this.mapdata(product);
    return this.http.post(this.baseUrl + 'api/product', data);
  }
  mapdata(product) {
    return {
      "name": product.name,
      "price": product.price,
      "imgName": product.imgName,
      "category_id": product.category_id,
      "band_id": product.band_id,
      "productChilViews": product.productChilViews
    }
  }
}
