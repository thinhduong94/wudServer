import { Component, OnInit } from '@angular/core';
import { ProductService } from '../service/product.service';
import { productView } from '../../assets/model/prouct';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: Array<productView> = [];
  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.getAll().subscribe(rp => {
      this.products = rp;
    })
  }

}
