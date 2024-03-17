export interface product {
    price : number;
    name : string;
    description : string;
    count : number
}

export interface Basket {
    id : number;
    products : product[];
    totalPrice : number;
    customer : Customer;
}

export interface Customer {
    id : number;
    firstName : string;
    lastName : string;
}