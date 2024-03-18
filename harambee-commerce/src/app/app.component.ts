import { Component, NO_ERRORS_SCHEMA, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommerceService } from './haramabee-servies/commerce.service';
import { Basket, Customer, product } from './models/product.model';
import { CommonModule } from '@angular/common'; 
import {MatTableModule} from '@angular/material/table';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCardModule} from '@angular/material/card';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule,MatTableModule,MatFormFieldModule,
     MatSelectModule, MatInputModule,MatCardModule, ReactiveFormsModule,FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [CommerceService],
  schemas : [NO_ERRORS_SCHEMA]
})
export class AppComponent implements OnInit{
  title = 'harambee-commerce'; 


  

  searchForm = new FormGroup({
    producSearchField : new FormControl('')
  });



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
  searchedProduct : product[] = [];
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

  Submit(){
    console.warn(this.searchForm.value);

    this.service.productSearch(this.searchForm.value.producSearchField)
    .subscribe(data => {
        if(data){
          this.searchedProduct = [data];
        }else{
          alert("Produc not found")
        }
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
