import React from 'react';
import { useState, useEffect } from "react";
import axios from 'axios';
import { BrowserRouter as Router,Switch,Route,Redirect, useNavigate} from "react-router-dom"

function Dashboard(){
    return(
        <>
            <h1>Welcome to the dashboard</h1>
        </>
    )
}

export default Dashboard;