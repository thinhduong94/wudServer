import { Component, OnInit } from '@angular/core';
import { OrderService } from '../service/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  orders = [];
  constructor(private orderService: OrderService) { }

  ngOnInit() {
    this.orderService.getAll().subscribe(data => {
      this.orders = data;
    })
  }

}
