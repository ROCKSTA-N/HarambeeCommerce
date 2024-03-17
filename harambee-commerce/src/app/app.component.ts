import { Component, NO_ERRORS_SCHEMA, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommerceService } from './haramabee-servies/commerce.service';
import { Basket, Customer, product } from './models/product.model';
import { CommonModule } from '@angular/common'; 
import {MatTableModule} from '@angular/material/table';
import {FormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule,MatTableModule,MatFormFieldModule, MatSelectModule, MatInputModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [CommerceService],
  schemas : [NO_ERRORS_SCHEMA]
})
export class AppComponent implements OnInit{
  title = 'harambee-commerce'; 
  products :product[] = [];
  customers :Customer[] = [];
  customerBasket: Basket = {
    id : 0,
     customer : {
      firstName : "",
      id : 0,
      lastName : ""
   },
    products : [],
    totalPrice : 0
  };
  displayedColumns: string[] = ['name', 'description', 'price', 'count'];
  CustomersDisplayedColumns: string[] = ['id', 'firstName', 'lastName']; 
  selectedCustomerId : number = -1 ;
  selectedCustomer : Customer = {
     firstName : "",
     id : 0,
     lastName : ""
  };
  basketvalue: number = 0;

  constructor( public service : CommerceService) {

  }
  ngOnInit(): void {
    this.service.GetProducts().subscribe(data => {
      this.products = data;
    });

    this.service.GetCcustomers().subscribe(data => {
      this.customers = data;
    });
  }

  onSelectEvent(customerId: number){
   this.selectedCustomerId = customerId;
    this.service.GetCustomerById(customerId)
    .subscribe(data => {
      this.selectedCustomer = data;

       this.service.GetCustomerBasket(customerId)
       .subscribe(basket => {
        this.customerBasket = basket;

          this.service.GetBasketValue(basket.id) .subscribe(basketvalue => {
            this.basketvalue = basketvalue;
          });
       });
    })
  }
}
