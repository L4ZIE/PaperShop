import React from 'react';
import PaperList from './Components/Paper/PaperList';
import NavBar from "./Components/NavBar/NavBar.tsx";

const Home: React.FC = () => {
    return (
        <div>
            <NavBar />
            <PaperList /> 
        </div>
    );
};

export default Home;
