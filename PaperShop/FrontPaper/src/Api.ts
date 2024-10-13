import {Paper} from "./Types.ts";

const API_URL = 'http://localhost:5201/api'; 

export const fetchPapers = async (): Promise<Paper[]> => {
    const response = await fetch(`${API_URL}/papers`);

    if (!response.ok) {
        throw new Error('Failed to fetch papers');
    }

    return response.json();
};