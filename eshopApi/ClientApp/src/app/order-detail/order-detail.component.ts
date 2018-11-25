import { Component, OnInit } from '@angular/core';
import { OrderService } from '../service/order.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent implements OnInit {
  order = {};
  constructor(private orderSv: OrderService, private route: ActivatedRoute) { }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.orderSv.getById(id)
      .subscribe(data => {
        console.log(id);
        this.order = data
      });
  }

}
