import React, { useEffect, useState } from 'react';
import PaperCard from './PaperCard';
import {Paper} from "../../../Types.ts";


const PaperList: React.FC = () => {
    const [papers, setPapers] = useState<Paper[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

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

    return (
        <div className="paper-list">
            {papers.map((paper) => (
                <PaperCard key={paper.id} paper={paper} />
            ))}
        </div>
    );
};

export default PaperList;