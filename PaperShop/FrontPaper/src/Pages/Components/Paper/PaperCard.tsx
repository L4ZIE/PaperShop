import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {Paper} from "../../../Types.ts";
import './PaperCard.css';
import { FaShoppingCart } from 'react-icons/fa';


interface PaperCardProps {
    paper: Paper;
}

const PaperCard: React.FC<PaperCardProps> = ({ paper }) => {
    const [quantity, setQuantity] = useState<number>(1);
    const navigate = useNavigate();

    // Handle quantity update
    const updateQuantity = (increment: boolean) => {
        setQuantity((prevQuantity) => {
            const newQuantity = increment ? prevQuantity + 1 : prevQuantity - 1;
            return newQuantity < 1 ? 1 : newQuantity; 
        });
    };

    const handleUpdate = () => {
        navigate(`/update-product/${id}`, { state: { paper, isEdit: true } });
    };

    const handleAddToCart = async () => {
        try {
            const response = await fetch('http://localhost:5201/api/OrderEntry', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    productId: paper.id,
                    quantity
                }),
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            alert('Item added to cart');
        } catch (error) {
            console.error('Failed to add to cart:', error);
            alert('Failed to add item to cart');
        }
    };

    const { name, price, properties = [], id } = paper;

    return (
        <div className="paper-card">
            {/* Paper name */}
            <div className="paper-header">
                <h3>{name}</h3>
            </div>

            {/* Properties */}
            {paper.properties && paper.properties.length > 0 ? (
                <div className="paper-properties">
                    <strong>Properties:</strong>
                    <ul>
                        {properties.map((property) => (
                            <li key={property.id}>{property.propertyName}</li>
                        ))}
                    </ul>
                </div>
            ) : (
                <div className="paper-properties empty"></div>  
            )}
            

            {/* Price */}
            <div className="paper-price">
                {price} kr
            </div>

            {/* Quantity Controls */}
            <div className="quantity-controls">
                <button onClick={() => updateQuantity(false)}
                        disabled={quantity === 1}>-
                </button>
                <span>{quantity}</span>
                <button onClick={() => updateQuantity(true)}>+</button>
            </div>

            {/* Add to cart button */}
            <div className="add-to-cart">
                <button className="cart-button" onClick={handleAddToCart}>
                    <FaShoppingCart/> 
                </button>
            </div>

            {/* Update button */}
            <div>
                <span className="action-link" onClick ={handleUpdate}>Update</span>
            </div>
            
        </div>
    );
};

export default PaperCard;
