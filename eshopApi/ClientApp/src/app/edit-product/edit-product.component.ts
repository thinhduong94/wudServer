import { Component, OnInit } from '@angular/core';
import { productView } from '../../assets/model/prouct';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';
import { CategoryService } from '../service/category.service';
import { BandService } from '../service/band.service';
import { ProductService } from '../service/product.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {

  public progress: number;
  public message: string;
  product: any = {};
  public size: any = {
    nameSize: '',
    valueSize: ''
  };
  productChilView: any = {
    sizeName: "",
    sizeValue: "",
    colorName: "",
    colorValue: ""
  };
  productChilViews: any[] = [];
  public categories = [];
  public bands = [];
  public color = {
    nameColor: '',
    valueColor: ''
  };
  public sizes = [];
  public colors = [];
  constructor(private http: HttpClient,
    private categoryService: CategoryService,
    private bandService: BandService,
    private productService: ProductService,
    private route: ActivatedRoute
  ) {
    this.categoryService.getAll().subscribe(rp => {
      this.categories = rp.data;
    });
    this.bandService.getAll().subscribe(rp => {
      this.bands = rp.data;
    })
  }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.productService.getById(id)
      .subscribe(data => {
        console.log(id);
        this.product = data.data
        this.productChilViews = this.product.productChilViews;
      });
  }
  add() {
    let count = this.productChilViews.filter(x => x.sizeValue == this.productChilView.sizeValue && x.colorValue == this.productChilView.colorValue).length;
    if (count == 0) {
      this.productChilViews.push({
        sizeName: this.productChilView.sizeName,
        sizeValue: this.productChilView.sizeValue,
        colorName: this.productChilView.colorName,
        colorValue: this.productChilView.colorValue
      });
    }
  }
  save() {
    this.product.productChilViews = this.productChilViews;
    this.productService.edit(this.product.id, this.product).subscribe(data => {
      console.log(data);
      alert("Success");
    });
  }
  upload(files) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (let file of files)
      formData.append(file.name, file);

    const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
      reportProgress: true,
    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response) {
        var obj: any = event.body;
        this.message = obj.mess;
        this.product.imgName = obj.nameFile;
      }
    });
  }
}
