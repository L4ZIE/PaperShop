import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './Pages/Home';
import PaperList from './Pages/Components/Paper/PaperList';
import './App.css'
import NewProduct from "./Pages/Components/NewProduct/NewProduct.tsx";
import UpdateProduct from "./Pages/Components/UpdateProduct/UpdateProduct.tsx";
import Cart from "./Pages/Cart/Cart.tsx";

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/papers" element={<PaperList />} />
                <Route path="/new-product" element={<NewProduct />} />
                <Route path="/update-product/:id" element={<UpdateProduct />} />
                <Route path="/cart" element={<Cart />} />
            </Routes>
        </Router>
    );
};

export default App;
