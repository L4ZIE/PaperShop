import React, { useEffect, useState } from 'react';
import './YourOrderHistory.css';
import NavBar from "../Components/NavBar/NavBar.tsx";
import { Order } from '../../Types';

interface Customer {
    id: number;
    name: string;
}

const YourOrderHistory: React.FC = () => {
    const [orders, setOrders] = useState<Order[]>([]);
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [selectedCustomerId, setSelectedCustomerId] = useState<number | null>(null);
    
    const fetchOrders = async () => {
        try {
            const response = await fetch('http://localhost:5201/api/Order');
            if (!response.ok) {
                throw new Error('Failed to fetch orders');
            }
            const data = await response.json();
            setOrders(data); // Update the orders state
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    };

    const fetchCustomers = async () => {
        try {
            const response = await fetch('http://localhost:5201/api/Customer');
            if (!response.ok) {
                throw new Error('Failed to fetch customers');
            }
            const data = await response.json();
            setCustomers(data);
        } catch (error) {
            console.error('Error fetching customers:', error);
        }
    };
    
    useEffect(() => {
        fetchOrders();
        fetchCustomers();
    }, []);
    
    const getCustomerName = (customerId?: number): string => {
        const customer = customers.find(c => c.id === customerId);
        return customer ? customer.name : 'Unknown';
    };

    const handleCustomerChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const value = event.target.value;
        setSelectedCustomerId(value === 'none' ? null : parseInt(value, 10));
    };
    
    const filteredOrders = selectedCustomerId !== null
        ? orders.filter(order => order.customerId === selectedCustomerId)
        :[];

    return (
        <div className="order-history">
            <NavBar />

            <h1>Your Order History</h1>

            <div >
                <select  id="customer-select" value={selectedCustomerId ?? 'none'} onChange={handleCustomerChange} className="customer_dropdown">
                    <option value="none">Select Customer</option>
                    {customers.map(customer => (
                        <option key={customer.id} value={customer.id}>{customer.name}</option>
                    ))}
                </select>
            </div>
            
            <table className="order-history-table">
                <thead>
                <tr>
                    <th>Order Number</th>
                    <th>Customer</th>
                    <th>Order Date</th>
                    <th>Delivery Date</th>
                    <th>Status</th>
                    <th>Total Amount</th>
                </tr>
                </thead>
                <tbody>
                {filteredOrders.map(order => (
                    <tr key={order.id}>
                        <td>{order.id}</td>
                        <td>{getCustomerName(order.customerId)}</td>
                        <td>{order.orderDate}</td>
                        <td>{order.deliveryDate}</td>
                        <td className={
                            order.status === 'pending'
                                ? 'pending-status'
                                : order.status === 'completed'
                                    ? 'completed-status'
                                    : ''
                        }>
                            {order.status}
                        </td>
                        <td>{order.totalAmount}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};

export default YourOrderHistory;
