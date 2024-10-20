import React, { useState, useEffect } from 'react';
import './UpdateProduct.css';
import { useNavigate, useLocation, useParams } from 'react-router-dom';
import NavBar from "../NavBar/NavBar.tsx";

const UpdateProduct: React.FC = () => {
    const { id } = useParams<{ id: string }>(); // Get product ID from the URL
    const [productId, setProductId] = useState<string | null>(null);
    const [productName, setProductName] = useState<string>('');
    const [price, setPrice] = useState<number | string>('');
    const [discontinued, setDiscontinued] = useState<string>('no');
    const [stock, setStock] = useState<number | string>('');
    const [customProperties, setCustomProperties] = useState<string>('');
    const [message, setMessage] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(true);

    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        const state = location.state as any;
        if (state && state.product) {
            const { id, name, price, discontinued, stock, properties } = state.product;
            setFormData(id, name, price, discontinued, stock, properties);
        } else if (id) {
            fetchProductData(id);
        }
    }, [location.state, id]);

    const setFormData = (id: string, name: string, price: number, discontinued: boolean, stock: number, properties: any) => {
        setProductId(id);
        setProductName(name);
        setPrice(price);
        setDiscontinued(discontinued ? 'yes' : 'no');
        setStock(stock);
        setCustomProperties(properties.map((prop: any) => prop.propertyName).join(', '));
        setLoading(false);
    };

    const fetchProductData = async (productId: string) => {
        try {
            const response = await fetch(`http://localhost:5201/api/Paper/${productId}`);
            if (response.ok) {
                const product = await response.json();
                const { id, name, price, discontinued, stock, properties } = product;
                setFormData(id, name, price, discontinued, stock, properties);
            } else {
                setMessage('Failed to fetch product data');
            }
        } catch (error) {
            setMessage('An error occurred while fetching product data');
        }
        setLoading(false);
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!productId) {
            setMessage('Invalid product ID');
            return;
        }

        const updatedProduct = {
            id: productId,
            name: productName,
            price: parseFloat(price as string),
            discontinued: discontinued === 'yes',
            stock: parseInt(stock as string),
            properties: customProperties.split(',').map((prop) => ({ propertyName: prop.trim() })),
        };

        try {
            const response = await fetch(`http://localhost:5201/api/Paper/${productId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedProduct),
            });

            if (response.ok) {
                setMessage('Product updated successfully!');
                navigate('/', { state: { successMessage: 'Product updated successfully!' } });
            } else {
                const errorData = await response.text();
                setMessage('Error updating product: ' + errorData);
            }
        } catch (error) {
            setMessage('An error occurred while updating the product');
        }
    };

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <div className="new-product">
            <NavBar />
            
            <h1 className="title">Update Product</h1>
            {message && <p>{message}</p>}
            <form className="product-form" onSubmit={handleSubmit}>
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

                <button type="submit" className="submit-button">Update Product</button>
            </form>
        </div>
    );
};

export default UpdateProduct;