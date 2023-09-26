import React from 'react';
import { GoogleLogin } from 'react-google-login';
import { useState, useEffect } from "react";
import axios from 'axios';
import { useNavigate} from "react-router-dom"

const clientId = "704267478812-snaf5fajvh8j62b5d16u781q4c8c2imv.apps.googleusercontent.com"

function Login(){
    const navigate = useNavigate();
    const [user, setUser] = new useState({});
    const [token, setToken] = new useState("");
    const [startTime, setStartTime] = new useState(0);
    const [endTime, setEndTime] = new useState(0);
    const [stepRecords, setStepRecords] = new useState([]);

    const onSuccess = (res) => {
        console.log("LOGIN SUCCES! Current user: ", res);
        setUser(res.profileObj);
        setToken(res.accessToken);
        //navigate("/dashboard");
    }

    useEffect(() => {
        if(token != ""){
            //scopesRequest();
        }
    }, [token]);

    useEffect(() => {
        if(user != null){
            saveUser();
        }
    }, [user]);

    const onFailure = (res) => {
        console.log("LOGIN FAIL! res: ", res);
    }

    function DatasourcesRequest(){
        axios.get("https://www.googleapis.com/fitness/v1/users/me/dataSources?dataTypeName=com.google.step_count.delta", {
            headers: { Authorization: 'Bearer ' + token }
        })
        .then(function (response) {
          console.log(response);
        });
    }

    function scopesRequest(){
        axios.get("https://www.googleapis.com/fitness/v1/users/me/dataSources/derived:com.google.step_count.delta:com.google.android.gms:estimated_steps/datasets/1694124000000000000-1694210400000000000", {
            headers: { Authorization: 'Bearer ' + token }
        })
        .then(function (response) {
            console.log(response);
            setStepRecords(response.data.point)
        });
    }

    function saveUser(){
        axios.post('https://localhost:7080/api/User', {
            name: user.name,
            email: user.email,
            accessToken: token
          })
          .then(function (response) {
            console.log(response);
          })
          .catch(function (error) {
            console.log(error);
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
                    //cookiePolicy={'single_host_origin'}
                    //isSignedIn={true}
                />
            </div>
        </>
     
    )
}

export default Login;