import React from 'react';
import './NavBar.css';
import { FaShoppingCart } from 'react-icons/fa';
import {useNavigate} from "react-router-dom";

const NavBar: React.FC = () => {
    const navigate = useNavigate();

    return (
        <nav className="navbar">
            <div className="nav-left">
                <button className="nav-button" onClick={() => navigate('/')}>Home</button>
                <button className="nav-button" onClick={() => navigate('/order-history')}>Order History</button>
                <button className="nav-button" onClick={() => navigate('/your-order-history')}>Your Order History</button>
            </div>
            <div className="nav-right">
                <button className="cart-page-button" onClick={() => navigate('/cart')}>
                    <FaShoppingCart/>
                </button>
            </div>
        </nav>
    );
}

export default NavBar;