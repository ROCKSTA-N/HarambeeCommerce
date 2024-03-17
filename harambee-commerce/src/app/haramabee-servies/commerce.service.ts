import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { Basket, Customer, product } from "../models/product.model";

@Injectable({providedIn: 'root'})
export class CommerceService{
 

    constructor(private http: HttpClient){}

    public GetProducts() : Observable<product[]>{
      return this.http.get<product[]>('http://localhost:5286/api/Product')
      .pipe(map(response => {
        return response;
      }));
    }

    public GetCcustomers() : Observable<Customer[]>{
        return this.http.get<Customer[]>('http://localhost:5286/api/Customer')
        .pipe(map(response => {
          return response;
        }));
      }

      public GetCustomerById(customerId :number) : Observable<Customer>{
        return this.http.get<Customer>(`http://localhost:5286/api/Customer/${customerId}`)
        .pipe(map(response => {
          return response;
        }));
      }

      public GetCustomerBasket(customerId: number) : Observable<Basket>{
        return this.http.get<Basket>(`http://localhost:5286/api/basket/Customer/${customerId}`)
        .pipe(map(response => {
          return response;
        }));
      }

      public GetBasketValue(basketId: number) : Observable<number>{
        return this.http.get<number>(`http://localhost:5286/api/basket/calculatevalue/${basketId}`)
        .pipe(map(response => {
          return response;
        }));
      }
}