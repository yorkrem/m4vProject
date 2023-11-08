import React from 'react';
import { useEffect, useState } from 'react';
import axios from '../components/axios';

function Home(){

    const [currentDateQuery, setCurrentDateQuery] = new useState("");
    const [steps, setSteps] = new useState(0);
    const [bmr, setBmr] = new useState(0);
    const [burnedCalories, setBurnedCalories] = new useState(0);

    function getCurrentDate(){
      const currentDate = new Date();
      const year = currentDate.getFullYear();
      const month = String(currentDate.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed, so we add 1 and pad with '0' if needed.
      const day = String(currentDate.getDate()).padStart(2, '0');
      const formattedDate = `${day}-${month}-${year}`;
      const query = "startdate="+"2023-11-06"
      setCurrentDateQuery(query);
    }

    useEffect(() => {
      getCurrentDate()
      }, []);


      useEffect(() => {
          if(currentDateQuery != ""){
               getSteps();
               getBurnedCalories();
               getBMR();
          }
      }, [currentDateQuery])


      async function getSteps(){
        axios.get("/Step?"+currentDateQuery)
        .then(function (response) {
          console.log(response.data.DailySteps);
          setSteps(response.data.DailySteps)
        })
        .catch(function(error) {
          console.log(error);
        })
      }

      async function getBurnedCalories(){
        axios.get("/BurnedCalories?"+currentDateQuery)
        .then(function (response) {
          console.log(response.data);
          setBurnedCalories(response.data.Calories)
        })
        .catch(function(error) {
          console.log(error);
        })
      }

      async function getBMR(){
        axios.get("/BMR?"+currentDateQuery)
        .then(function (response) {
          console.log(response.data);
          setBmr(response.data.Calories)
        })
        .catch(function(error) {
          console.log(error);
        })
      }

      return (
        <div>
          <h4>Todays Total Steps:</h4>
          {steps} 
          <h4>Todays Total BurnedCalories:</h4>
          {burnedCalories}
          <h4>Todays Total BMR:</h4>
          {bmr}
        </div>
        );
}

export default Home;