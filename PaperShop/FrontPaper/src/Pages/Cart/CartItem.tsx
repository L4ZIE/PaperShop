import React from 'react';
import './Cart.css'

type CartItemProps = {
    item: {
        id: number;
        name: string;
        price: number;
        quantity: number;
    };
    increment: (id: number) => void;
    decrement: (id: number) => void;
    removeItem: (id: number) => void;
};

const CartItem: React.FC<CartItemProps> = ({ item, increment, decrement, removeItem }) => {
    return (
        <div className="cart-item">
            <div className="cart-item__product">{item.name}</div>
            <div className="cart-item__price">{item.price} kr</div>
            <div className="cart-item__quantity">
                <button onClick={() => decrement(item.id)}>-</button>
                <span>{item.quantity}</span>
                <button onClick={() => increment(item.id)}>+</button>
            </div>
            <div className="cart-item__total">{item.price * item.quantity} kr</div>
            <button onClick={() => removeItem(item.id)}>Remove</button>
        </div>
    );
};

export default CartItem;