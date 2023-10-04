import React from 'react';
import { GoogleLogin } from 'react-google-login';
import { useState, useEffect } from "react";
import axios from 'axios';
import { useNavigate} from "react-router-dom"

const clientId = "704267478812-snaf5fajvh8j62b5d16u781q4c8c2imv.apps.googleusercontent.com"

function Login(){
    const navigate = useNavigate();
    const [token, setToken] = new useState("");
    const [startTime, setStartTime] = new useState(0);
    const [endTime, setEndTime] = new useState(0);

    //Saved Data
    const [user, setUser] = new useState({});
    const [stepRecords, setStepRecords] = new useState(0);
    const [burnedCalories, setBurnedCalories] = new useState(0);
    const [moveMinutes, setMoveMinutes] = new useState(0);

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
            stepsRequest();
            burnedCaloriesRequest();
            moveMinutesRequest();
        }
    }, [startTime, endTime]);
    
    //Start Steps Segment
    function stepsRequest(){
        axios.post("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate", 
        {
            "aggregateBy": [{
                "dataTypeName": "com.google.step_count.delta",
                "dataSourceId": "derived:com.google.step_count.delta:com.google.android.gms:estimated_steps"
              }],
              "bucketByTime": { "durationMillis": 86400000 },
              "startTimeMillis": startTime,
              "endTimeMillis": endTime
        },{
            headers: { Authorization: 'Bearer ' + token }
        })
        .then(function (response) {
          //console.log(response.data.bucket[0].dataset[0].point[0].value[0].intVal)
          setStepRecords(response.data.bucket[0].dataset[0].point[0].value[0].intVal)
        });
    }

    useEffect(() => {
        if(stepRecords > 0){
            saveSteps();
        }
    }, [stepRecords]);

    function saveSteps(){
        axios.post('https://localhost:7212/api/Step', {
            DailySteps: stepRecords,
            StartTimeNanos: startTime,
            EndTimeNanos: endTime,
            UserEmail: user.email
          })
          .catch(function (error) {
            console.log(error);
          });
    }

    //End Steps Segment

    //Start BurnedCalories Segment
    function burnedCaloriesRequest(){
        axios.post("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate", 
        {
            "aggregateBy": [{
                "dataTypeName": "com.google.calories.expended",
                "dataSourceId": "derived:com.google.calories.expended:com.google.android.gms:merge_calories_expended"
              }],
              "bucketByTime": { "durationMillis": 86400000 },
              "startTimeMillis": startTime,
              "endTimeMillis": endTime
        },{
            headers: { Authorization: 'Bearer ' + token }
        })
        .then(function (response) {
          //console.log(response.data.bucket[0].dataset[0].point[0].value[0].fpVal)
          setBurnedCalories(response.data.bucket[0].dataset[0].point[0].value[0].fpVal)
        });
    }

    useEffect(() => {
        if(burnedCalories > 0){
            saveBurnedCalories();
        }
    }, [burnedCalories]);

    function saveBurnedCalories(){
        axios.post('https://localhost:7212/api/BurnedCalories', {
            Calories: burnedCalories,
            StartTimeNanos: startTime,
            EndTimeNanos: endTime,
            UserEmail: user.email
          })
          .catch(function (error) {
            console.log(error);
          });
    }
    //End BurnedCalories Segment

    //Start MoveMinutes Segment

    function moveMinutesRequest(){
        axios.post("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate", 
        {
            "aggregateBy": [{
                "dataTypeName": "com.google.active_minutes",
                "dataSourceId": "derived:com.google.active_minutes:com.google.android.gms:merge_active_minutes"
              }],
              "bucketByTime": { "durationMillis": 86400000 },
              "startTimeMillis": startTime,
              "endTimeMillis": endTime
        },{
            headers: { Authorization: 'Bearer ' + token }
        })
        .then(function (response) {
          //console.log(response.data.bucket[0].dataset[0].point[0].value[0].intVal)
          setMoveMinutes(response.data.bucket[0].dataset[0].point[0].value[0].intVal)
        });
    }

    useEffect(() => {
        if(moveMinutes > 0){
            saveMoveMinutes();
        }
    }, [moveMinutes]);

    function saveMoveMinutes(){
        axios.post('https://localhost:7212/api/MoveMinutes', {
            MoveMinutes: moveMinutes,
            StartTime: startTime,
            EndTime: endTime,
            UserEmail: user.email
          })
          .catch(function (error) {
            console.log(error);
          });
    }
    //End MoveMinutes Segment
    //Start User Segment
    useEffect(() => {
        if(user != null){
            saveUser();
        }
    }, [user]);

    
    function saveUser(){
        axios.post('https://localhost:7212/api/User', {
            name: user.name,
            email: user.email,
            accessToken: token
          })
          .catch(function (error) {
            console.log(error);
          });
    }

    //End User Segment

 

    function DatasourcesRequest(){
        axios.get("https://www.googleapis.com/fitness/v1/users/me/dataSources?dataTypeName=com.google.active_minutes", {
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
                    //cookiePolicy={'single_host_origin'}
                    //isSignedIn={true}
                />
            </div>
        </>
     
    )
}

export default Login;