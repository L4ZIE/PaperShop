import React, { useEffect, useState } from 'react';
import './NewProperty.css';
import { useNavigate } from 'react-router-dom';
import NavBar from "../NavBar/NavBar.tsx";

interface Property {
    id: number;
    propertyName: string;
}

const NewProperty: React.FC = () => {
    const [propertyName, setPropertyName] = useState<string>('');
    const [properties, setProperties] = useState<Property[]>([]);
    const [message, setMessage] = useState<string>('');

    const navigate = useNavigate();
    const apiUrl = 'http://localhost:5201/api/property';

    const fetchProperties = async () => {
        try {
            const response = await fetch(`${apiUrl}`);
            const data = await response.json();
            setProperties(data);
        } catch (error) {
            console.error('Error fetching properties:', error);
        }
    };

    useEffect(() => {
        fetchProperties();
    }, []);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const newProperty = { propertyName };

        try {
            const response = await fetch(`${apiUrl}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newProperty),
            });

            if (response.ok) {
                setMessage('Property successfully created!');
                setPropertyName('');
                fetchProperties();
            } else {
                setMessage('Error creating property');
            }
        } catch (error) {
            setMessage('An error occurred while creating the property');
            console.error('Error creating property:', error);
        }
    };

    const handleUpdate = async (id: number) => {
        const updatedProperty = { propertyName: prompt("Enter new property name:") };

        if (!updatedProperty.propertyName) {
            alert("Property name cannot be empty.");
            return;
        }

        try {
            const response = await fetch(`${apiUrl}/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedProperty),
            });

            if (response.ok) {
                setMessage('Property successfully updated!');
                fetchProperties();
            } else {
                setMessage('Error updating property');
            }
        } catch (error) {
            setMessage('An error occurred while updating the property');
            console.error('Error updating property:', error);
        }
    };

    const handleDelete = async (id: number) => {
        if (!window.confirm('Are you sure you want to delete this property?')) return;

        try {
            const response = await fetch(`${apiUrl}/${id}`, {
                method: 'DELETE',
            });

            if (response.ok) {
                setMessage('Property successfully deleted!');
                fetchProperties();
            } else {
                setMessage('Error deleting property');
            }
        } catch (error) {
            setMessage('An error occurred while deleting the property');
            console.error('Error deleting property:', error);
        }
    };

    return (
        <div className="new-property">
            <NavBar />
            <h1 className="title">New Property</h1>
            {message && <p>{message}</p>}
            <form className="property-form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Property Name</label>
                    <input
                        type="text"
                        value={propertyName}
                        onChange={(e) => setPropertyName(e.target.value)}
                        className="input-field"
                        placeholder="Enter property name"
                        required
                    />
                </div>
                <button type="submit" className="submit-button">Add Property</button>
            </form>
            <h2 className="table-title">Property List</h2>
            <table className="property-table">
                <thead>
                <tr>
                    <th>ID</th>
                    <th>Property Name</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                {properties.map((property) => (
                    <tr key={property.id}>
                        <td>{property.id}</td>
                        <td>{property.propertyName}</td>
                        <td>
                            <button className="action-button" onClick={() => handleUpdate(property.id)}>
                                Update
                            </button>
                            <button className="action-button delete" onClick={() => handleDelete(property.id)}>
                                Delete
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};

export default NewProperty;
