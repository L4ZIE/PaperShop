import React, { useEffect, useState } from 'react';
import PaperCard from './PaperCard';
import {Paper} from "../../../Types.ts";
import './PaperList.css';
import {useNavigate} from "react-router-dom";


const PaperList: React.FC = () => {
    const [papers, setPapers] = useState<Paper[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [filter, setFilter] = useState<string>('');
    const navigate = useNavigate();

    useEffect(() => {
        const fetchPapers = async () => {
            try {
                const response = await fetch('http://localhost:5201/api/Paper'); 
                if (!response.ok) {
                    throw new Error('Failed to fetch papers');
                }
                const data: Paper[] = await response.json();
                setPapers(data);
            }  catch (err: unknown) {
                if (err instanceof Error) {
                    setError(err.message);
                } else {
                    setError('An unknown error occurred');
                }
            } finally {
                setLoading(false);
            }
        };

        fetchPapers();
    }, []);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    const filteredPapers = papers
        .filter((paper) =>
            paper.name.toLowerCase().includes(searchTerm.toLowerCase())
        )
        .sort((a, b) => {
            if (filter === 'priceLowHigh') return a.price - b.price;
            if (filter === 'priceHighLow') return b.price - a.price;
            return 0;
        });

    return (
        <div className="paper-list">
            <input
                type="text"
                placeholder="Search Papers"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="search-bar"
            />
            <select value={filter} onChange={(e) => setFilter(e.target.value)} className="filter-dropdown">
                <option value="">Sort By</option>
                <option value="priceLowHigh">Price: Low to High</option>
                <option value="priceHighLow">Price: High to Low</option>
            </select>
            <button onClick={() => navigate('/new-product')} className="add-product-button">
                Add product
            </button>
            <div className="paper-table">
                {filteredPapers.map((paper) => (
                    <PaperCard key={paper.id} paper={paper}/>
                ))}
            </div>
        </div>
    );
};

export default PaperList;