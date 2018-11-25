import { Component, OnInit } from '@angular/core';
import { ProductService } from '../service/product.service';
import { productView } from '../../assets/model/prouct';
import { SearchService } from '../service/search.service';
import { forkJoin } from 'rxjs/observable/forkJoin';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: Array<productView> = [];
  searchItem = {
    name: "",
    price: "",
    band: "",
    category: "",
    size: "",
    color: "",
    priceFrom: "",
    priceTo: ""
  }
  bands: any[] = [];
  cateroies: any[] = [];
  color: any[] = [];
  size: any[] = [];

  constructor(private searchSv: SearchService,private productService: ProductService) { }

  ngOnInit() {
    this.productService.getAll().subscribe(rp => {
      this.products = rp;
    });
    this.searchSv.getBands().subscribe(data => {
      this.bands = data.data;
    });
    this.searchSv.getCategories().subscribe(data => {
      this.cateroies = data.data;
    });
  }
  clear() {
    this.searchItem = {
      name: "",
      price: "",
      priceFrom: "",
      priceTo: "",
      band: "",
      category: "",
      size: "",
      color: ""
    };
  }
  change() {
    console.log(1);
  }
  getSearch() {
    forkJoin([
      this.searchSv.getSize(this.searchItem.category),
      this.searchSv.getColor(this.searchItem.category)
    ]).subscribe((results: any[]) => {
      console.log(results);
      this.size = results[0];
      this.color = results[1];
    })
  }
  search() {
    this.searchSv.search(this.searchItem).subscribe(data => {
      this.products = data;
    });
  }
}
