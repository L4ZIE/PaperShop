import React, { useState } from 'react';
import './NewProduct.css';

import { useNavigate } from 'react-router-dom';
import NavBar from "../NavBar/NavBar.tsx";

const NewProduct: React.FC = () => {
    const [productName, setProductName] = useState<string>('');
    const [price, setPrice] = useState<number | string>('');
    const [discontinued, setDiscontinued] = useState<string>('');
    const [stock, setStock] = useState<number | string>('');
    const [customProperties, setCustomProperties] = useState<string>('');
    const [message, setMessage] = useState<string>('');

    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const newProduct = {
            name: productName,
            price: parseFloat(price as string),
            discontinued: discontinued === 'yes',
            stock: parseInt(stock as string),
            properties: customProperties.split(',').map((prop) => ({ propertyName: prop.trim() })),
        };

        console.log("New product data: ", newProduct);

        try {
            const response = await fetch('http://localhost:5201/api/Paper', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newProduct),
            });

            console.log("Response Status:", response.status);

            if (response.ok){
                console.log("Product created successfully!");

                setMessage('Product successfully created!');
                setProductName('');
                setPrice('');
                setDiscontinued('no');
                setStock('');
                setCustomProperties('');

                // Redirect to main page with success message
                navigate('/', { state: { successMessage: 'Product successfully created!' } });
            } else {
                const errorData = await response.text();
                console.error("Error creating product, response data:", errorData);
                setMessage('Error creating product');
            }
        } catch (error) {
            console.error("Error: ", error);
            setMessage('An error occurred while creating the product');
        }
    };


    return (
        <div className="new-product">
            <NavBar />
            
            <h1 className="title">New product</h1>
            {message && <p>{message}</p>} {/* Success/Error message */}
            <form className="product-form" onSubmit={handleSubmit}>
                {/* Product Name */}
                <div className="form-group">
                    <label>Name of the product</label>
                    <input
                        type="text"
                        value={productName}
                        onChange={(e) => setProductName(e.target.value)}
                        className="input-field1"
                        placeholder="Enter product name"
                    />
                </div>

                {/* Price */}
                <div className="form-group">
                    <label>Price</label>
                    <input
                        type="number"
                        value={price}
                        onChange={(e) => setPrice(e.target.value)}
                        className="input-field"
                        placeholder="Enter price"
                    />
                    <span className="currency-label"> kr</span>
                </div>

                {/* Discontinued */}
                <div className="form-group">
                    <label>Discontinued</label>
                    <div className="checkbox-group">
                        <label>
                            <input
                                type="radio"
                                value="yes"
                                checked={discontinued === 'yes'}
                                onChange={() => setDiscontinued('yes')}
                            />
                            Yes
                        </label>
                        <label>
                            <input
                                type="radio"
                                value="no"
                                checked={discontinued === 'no'}
                                onChange={() => setDiscontinued('no')}
                            />
                            No
                        </label>
                    </div>
                </div>

                {/* Stock */}
                <div className="form-group">
                    <label>Stock</label>
                    <input
                        type="number"
                        value={stock}
                        onChange={(e) => setStock(e.target.value)}
                        className="input-field"
                        placeholder="Enter stock quantity"
                    />
                </div>

                {/* Custom Properties */}
                <div className="form-group">
                    <label>
                        Custom properties <span className="optional-label">*Optional</span>
                    </label>
                    <input
                        type="text"
                        value={customProperties}
                        onChange={(e) => setCustomProperties(e.target.value)}
                        className="input-field"
                        placeholder="Enter custom properties"
                    />
                </div>

                {/* Submit Button */}
                <button type="submit" className="submit-button">Add product</button>
            </form>
        </div>
    );
};

export default NewProduct;