import logo from './logo.svg';
import './App.css';
import Dashboard from './pages/dashboard';
import Home from './pages/home';
import { useEffect } from 'react';
import { gapi } from 'gapi-script';
import { Router,Switch,Route,Redirect, useNavigate, Routes} from "react-router-dom";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<Home/>}></Route>
        <Route path="/dashboard" element={<Dashboard/>}></Route>
      </Routes>
    </div>
  );
}

export default App;
