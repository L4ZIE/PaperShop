export interface Paper {
    id: number;
    name: string;
    price: number;
    discontinued: boolean;
    stock: number;
    properties?: Property[];
}

export interface Property {
    id: number;
    propertyName: string;
}

export interface OrderEntry {
    id: number;
    quantity: number;
    productId: number;
    orderId?: number;
    product?: Paper;
}

export interface Order {
    id: number;
    deliveryDate?: string;
    totalAmount: number;
    customerId?: number;
    status: string;
    orderDate: string;
    orderEntries: OrderEntry[];
}
