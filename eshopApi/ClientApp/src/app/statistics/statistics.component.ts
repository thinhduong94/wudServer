import { Component, OnInit } from '@angular/core';
import { OrderService } from '../service/order.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {
  input = {
    "dateFrom": "",
    "dateTo": ""
  }
  output = {};
  constructor(private orderService: OrderService) { }

  ngOnInit() {
  }
  run() {
    this.orderService.statistics(this.input).subscribe(data => {
      this.output = data;
    })
  }

}
