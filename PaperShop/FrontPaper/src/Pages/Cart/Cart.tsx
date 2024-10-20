import React, { useEffect, useState } from 'react';
import axios from 'axios';
import CartItem from './CartItem';
import './Cart.css';
import { OrderEntry } from '../../Types';
import NavBar from "../Components/NavBar/NavBar.tsx"; 

const Cart: React.FC = () => {
    const [cartItems, setCartItems] = useState<OrderEntry[]>([]);
    const [selectedCustomer, setSelectedCustomer] = useState<number | null>(null);
    const [customers, setCustomers] = useState<{ id: number, name: string }[]>([]);

    useEffect(() => {
        // Load cart items from the backend
        axios.get('http://localhost:5201/api/OrderEntry')
            .then(response => {
                if (Array.isArray(response.data)) {
                    setCartItems(response.data);
                } else {
                    console.error('Unexpected response format:', response.data);
                    setCartItems([]);
                }
            })
            .catch(error => {
                console.error('Failed to load cart items:', error);
                setCartItems([]);
            });
        
        // Load customers 
        axios.get('http://localhost:5201/api/Customer')
            .then(response => {
                if (Array.isArray(response.data)) {
                    setCustomers(response.data);
                } else {
                    console.error('Unexpected response format:', response.data);
                    setCustomers([]);
                }
            })
            .catch(error => {
                console.error('Failed to load customers:', error);
                setCustomers([]);
            });
    }, []);

    const updateCartItemQuantity = async (id: number, quantity: number) => {
        try {
            await axios.put(`http://localhost:5201/api/OrderEntry/${id}`, { quantity });
        } catch (error) {
            console.error('Failed to update cart item:', error);
        }
    };

    const incrementQuantity = (id: number) => {
        setCartItems(prevItems =>
            prevItems.map(item =>
                item.id === id ? { ...item, quantity: item.quantity + 1 } : item
            )
        );
        const updatedItem = cartItems.find(item => item.id === id);
        if (updatedItem) {
            updateCartItemQuantity(id, updatedItem.quantity + 1);
        }
    };

    const decrementQuantity = (id: number) => {
        setCartItems(prevItems =>
            prevItems.map(item =>
                item.id === id && item.quantity > 1
                    ? { ...item, quantity: item.quantity - 1 } : item
            )
        );
        const updatedItem = cartItems.find(item => item.id === id);
        if (updatedItem && updatedItem.quantity > 1) {
            updateCartItemQuantity(id, updatedItem.quantity - 1);
        }
    };

    const removeCartItem = async (id: number) => {
        try {
            await axios.delete(`http://localhost:5201/api/OrderEntry/${id}`);
        } catch (error) {
            console.error('Failed to remove cart item:', error);
        }
    };

    const removeItem = (id: number) => {
        setCartItems(prevItems => prevItems.filter(item => item.id !== id));
        removeCartItem(id);
    };

    const totalPrice = cartItems.reduce((total, item) => total + (item.product?.price ?? 0) * item.quantity, 0);

    return (
        <div className="cart-container">
            <NavBar/>
            <h2 className="cart-title">Your cart items</h2>

            <select onChange={(e) => setSelectedCustomer(Number(e.target.value))} className="customer-dropdown">
                <option value="">Select a customer</option>
                {customers.map((customer) => (
                    <option key={customer.id} value={customer.id}>
                        {customer.name}
                    </option>
                ))}
            </select>

            {cartItems.map((item: OrderEntry) => (
                <CartItem
                    key={item.id}
                    item={{
                        id: item.id,
                        name: item.product?.name || 'Unknown',
                        price: item.product?.price || 0,
                        quantity: item.quantity
                    }}
                    increment={incrementQuantity}
                    decrement={decrementQuantity}
                    removeItem={removeItem}
                />
            ))}

            <div className="cart-footer">
                <h3>Total price: {totalPrice} kr</h3>
                <button className="checkout-button" disabled={!selectedCustomer}>Check-out</button>
            </div>
        </div>
    );
};

export default Cart;

