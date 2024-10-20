import React, {useEffect, useState} from 'react';
import './OrderHistory.css';
import NavBar from "../Components/NavBar/NavBar.tsx";
import {Order, Customer} from '../../Types';

const OrderHistory: React.FC = () => {
    const [orders, setOrders] = useState<Order[]>([]);
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);
    const [isModalOpen, setIsModalOpen] = useState(false);


    const fetchOrders = async () => {
        try {
            const response = await fetch('http://localhost:5201/api/Order');
            if (!response.ok) {
                throw new Error('Failed to fetch orders');
            }
            const data = await response.json();
            setOrders(data);
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
            setCustomers(data); // Update the customers state
        } catch (error) {
            console.error('Error fetching customers:', error);
        }
    };

    // Fetch orders when the component loads
    useEffect(() => {
        fetchOrders();
        fetchCustomers();
    }, []);
    
    const getCustomerName = (customerId?: number): string => {
        const customer = customers.find(c => c.id === customerId);
        return customer ? customer.name : 'Unknown';
    };

    const openChangeStatusModal = (order: Order) => {
        setSelectedOrder(order);
        setIsModalOpen(true);
    };
    
    const confirmChangeStatus = async () => {
        if (!selectedOrder) return;

        try {
            const updatedOrder = { ...selectedOrder, status: 'completed' };
            const response = await fetch(`http://localhost:5201/api/Order/${selectedOrder.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedOrder),
            });

            if (!response.ok) {
                throw new Error('Failed to update order status');
            }

            // Update the order status in the state
            setOrders(prevOrders =>
                prevOrders.map(order =>
                    order.id === selectedOrder.id ? { ...order, status: 'completed' } : order
                )
            );
            
            setIsModalOpen(false);
            setSelectedOrder(null);
        } catch (error) {
            console.error('Error updating order status:', error);
        }
    };

    // Handle closing the modal
    const closeModal = () => {
        setIsModalOpen(false);
        setSelectedOrder(null);
    };
    
    return (
        <div className="order-history">
            <NavBar />

            <h1>Order History</h1>

            {isModalOpen && selectedOrder && (
                <div className="modal">
                    <div className="modal-content">
                        <p>Are you sure you want to change the status of Order #{selectedOrder.id} to "completed"?</p>
                        <button onClick={confirmChangeStatus}>Yes, Change Status</button>
                        <button onClick={closeModal}>Cancel</button>
                    </div>
                </div>
            )}
            
            <table className="order-history-table">
                <thead>
                <tr>
                    <th>Order number</th>
                    <th>Customer</th>
                    <th>Order date</th>
                    <th>Delivery Date</th>
                    <th>Status</th>
                    <th>Total amount</th>
                </tr>
                </thead>
                <tbody>
                {orders.map((order) => (
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
                            {order.status === 'pending' && (
                                <button onClick={() => openChangeStatusModal(order)} className="change-status-btn">Change status</button>
                            )}
                        </td>
                        <td>{order.totalAmount}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};

export default OrderHistory;