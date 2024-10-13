import React from 'react';
import PaperList from './Components/Paper/PaperList';

const Home: React.FC = () => {
    return (
        <div className="home-container">
            <h1 className="title">Welcome to Dunder Mifflin Infinity!</h1>
            <h2 className="subtitle">Available Papers</h2>
            <PaperList /> 
        </div>
    );
};

export default Home;
