import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class OrderService {

  baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }
  public getAll() {
    return this.http.get<any>(this.baseUrl + 'api/order');
  }
  public getById(id) {
    return this.http.get<any>(this.baseUrl + 'api/order/' + id);
  }
  public statistics(input) {
    return this.http.post<any>(this.baseUrl + 'api/order/statistics', this.mapdate(input));
  }
  mapdate(item) {
    return {
      "dateFrom": item.dateFrom || null,
      "dateTo": item.dateTo || null

    }
  }
}
