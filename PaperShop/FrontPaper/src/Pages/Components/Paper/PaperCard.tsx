import React from 'react';
import {Paper} from "../../../Types.ts";


interface PaperCardProps {
    paper: Paper;
}

const PaperCard: React.FC<PaperCardProps> = ({ paper }) => {
    return (
        <div className="paper-card">
            <h3 className="paper-title">{paper.name}</h3>
            <p className="paper-price">${paper.price.toFixed(2)}</p>
            <p className="paper-stock">In Stock: {paper.stock}</p>
            <p className="paper-discontinued">
                {paper.discontinued ? 'Discontinued' : 'Available'}
            </p>
            <div className="paper-properties">
                <h4>Properties:</h4>
                <ul>
                    {paper.properties.map((property) => (
                        <li key={property.id}>{property.propertyName}</li>
                    ))}
                </ul>
            </div>
        </div>
    );
};

export default PaperCard;