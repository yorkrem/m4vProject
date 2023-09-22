import { GoogleLogin } from 'react-google-login';
import { useState } from "react";
import axios from 'axios';

const clientId = "704267478812-snaf5fajvh8j62b5d16u781q4c8c2imv.apps.googleusercontent.com"

function Login(){
    const [user, setUser] = new useState({});
    const [token, setToken] = new useState("");
    const [startTime, setStartTime] = new useState(0);
    const [endTime, setEndTime] = new useState(0);
    const [stepRecords, setStepRecords] = new useState([]);
    const [dailySteps, setDailySteps] = new useState(0);
    const onSuccess = (res) => {
        console.log("LOGIN SUCCES! Current user: ", res);
        setDailySteps(0);
        setUser(res);
        setToken(res.accessToken);
        scopesRequest();
    }

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
          calculateDailySteps();
        });
    }


    function calculateDailySteps(){
        stepRecords.map((record) => {
            setDailySteps(dailySteps  => dailySteps + record.value[0].intVal)
        });
    }

    return(
        <>
           <div id="signInButton">
                <GoogleLogin
                    clientId={clientId}
                    buttonText='Login'
                    onSuccess={onSuccess}
                    onFailure={onFailure}
                    cookiePolicy={'single_host_origin'}
                    isSignedIn={true}
                />
            </div>
            <div>
                <h4>{dailySteps}</h4>
            </div>
        </>
     
    )
}

export default Login;