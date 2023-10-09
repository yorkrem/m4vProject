import React from 'react';
import { GoogleLogin } from 'react-google-login';
import { useState, useEffect } from "react";
import { useNavigate} from "react-router-dom"
import axios from "axios"
import { stepsRequest } from '../data/steps';
import { burnedCaloriesRequest } from '../data/caloriesBurnt';
import { moveMinutesRequest } from '../data/moveMinutes';
import { heartPointsRequest } from '../data/heartPoints';


const clientId = "704267478812-snaf5fajvh8j62b5d16u781q4c8c2imv.apps.googleusercontent.com"

function Login(){
    const navigate = useNavigate();
    const [token, setToken] = new useState("");
    const [startTime, setStartTime] = new useState(0);
    const [endTime, setEndTime] = new useState(0);

    //Saved Data
    const [user, setUser] = new useState({});

    //Start Login Segment
    const onSuccess = (res) => {
        console.log("LOGIN SUCCES! Current user: ", res);
        setUser(res.profileObj);
        setToken(res.accessToken);
        //navigate("/dashboard");
    }

    const onFailure = (res) => {
        console.log("LOGIN FAIL! res: ", res);
    }
    //End Login Segment

   
    useEffect(() => {
        if(token != ""){
            calculateStartEndTime();
            //DatasourcesRequest()
        }
    }, [token]);

    function calculateStartEndTime(){
        const currentDate = new Date();
        currentDate.setHours(0, 0 ,0 ,0);
        setStartTime(currentDate.getTime());
        currentDate.setHours(23, 59, 59, 999);
        setEndTime(currentDate.getTime());
    }

    useEffect(() => {
        if(startTime != 0 && endTime != 0){
            initializeData();
        }
    }, [startTime, endTime]);

    function initializeData(){
        stepsRequest(token, startTime, endTime, user)
        burnedCaloriesRequest(token, startTime, endTime, user)
        moveMinutesRequest(token, startTime, endTime, user)
        heartPointsRequest(token, startTime, endTime, user)
    }

    useEffect(() => {
        if(user != null){
            saveUser();
        }
    }, [user]);

    
    async function saveUser(){
        await axios.post('https://localhost:7212/api/User', {
            name: user.name,
            email: user.email,
            accessToken: token
          })
          .catch(function (error) {
            console.log(error);
          });
    }

    function DatasourcesRequest(){
        axios.get("https://www.googleapis.com/fitness/v1/users/me/dataSources?dataTypeName=com.google.heart_minutes", {
            headers: { Authorization: 'Bearer ' + token }
        })
        .then(function (response) {
          console.log(response);
        });
    }


    return(
        <>
           <h1>Verbind je gezondheidsdata</h1>
           <div id="signInButton">
                <GoogleLogin
                    clientId={clientId}
                    buttonText='Koppel'
                    onSuccess={onSuccess}
                    onFailure={onFailure}
                    cookiePolicy={'single_host_origin'}
                    isSignedIn={true}
                />
            </div>
        </>
     
    )
}

export default Login;