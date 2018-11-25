import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UploadComponent } from './upload/upload.component';
import { ProductComponent } from './product/product.component';
import { ProductService } from './service/product.service';
import { CreateComponent } from './product/create/create.component';
import { CategoryService } from './service/category.service';
import { BandService } from './service/band.service';
import { OrderComponent } from './order/order.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { OrderService } from './service/order.service';
import { EditProductComponent } from './edit-product/edit-product.component';
import { SearchService } from './service/search.service';
import { StatisticsComponent } from './statistics/statistics.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UploadComponent,
    ProductComponent,
    CreateComponent,
    OrderComponent,
    OrderDetailComponent,
    EditProductComponent,
    StatisticsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'upload', component: UploadComponent },
      { path: 'product', component: ProductComponent },
      { path: 'product/:id', component: EditProductComponent },
      { path: 'create', component: CreateComponent },
      { path: 'order', component: OrderComponent },
      { path: 'order/:id', component: OrderDetailComponent },
      { path: 'statistics', component: StatisticsComponent }
    ])
  ],
  providers: [ProductService, CategoryService, BandService, OrderService, SearchService],
  bootstrap: [AppComponent]
})
export class AppModule { }
