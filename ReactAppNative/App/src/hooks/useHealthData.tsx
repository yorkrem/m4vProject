import axios from 'axios';
import { useEffect, useState } from 'react';
import AppleHealthKit, {HealthInputOptions, HealthKitPermissions, HealthValue} from 'react-native-health';
import { Platform } from 'react-native';
import { initialize, readRecords, requestPermission } from 'react-native-health-connect';
import { TimeRangeFilter } from 'react-native-health-connect/lib/typescript/types/base.types';

const permissions: HealthKitPermissions = {
    permissions: {
        read: [AppleHealthKit.Constants.Permissions.Steps, AppleHealthKit.Constants.Permissions.BasalEnergyBurned, AppleHealthKit.Constants.Permissions.ActiveEnergyBurned],
        write: []
    }
  }

const useHealthData = (date: Date) => {
  const [hasPermissions, setHasPermissions] = useState(false);
  const [steps, setSteps] = useState(0);
  const [exerciseTime, setExerciseTime] = useState({});
  const [basalEnergyBurned, setBasalEnergyBurned] = useState(0);
  const [caloriesBurned, setCaloriesBurned] = useState(0);
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("")
  const [message, setMessage] = useState("");


  //IOS-HEALTHKIT
  useEffect(() => {
    if(Platform.OS != 'ios'){
      return;
    }
    AppleHealthKit.isAvailable((error, isAvailable) => {
      if(error){
        console.log("error checking availability")
        return;
      }
      if(!isAvailable){
        console.log("Appple Health not available");
        return;
      }
      AppleHealthKit.initHealthKit(permissions, (error) => {
        if(error){
          console.log(error);
          return;
        }
        setHasPermissions(true);
      })
    })
  }, [])

  useEffect(() => {
    if(!hasPermissions){
      return;
    }
    const options: HealthInputOptions = {
      date: date.toISOString(),
      includeManuallyAdded: false,
    };
    AppleHealthKit.getStepCount(options,(error, results) => {
      if(error){
        console.log("error getting steps");
        return;
      }
      setSteps(results.value)
    });
    AppleHealthKit.getAppleExerciseTime(options, (error, results) => {
      if(error){
        console.log("error getting exercise time");
        return;
      }
      //setExerciseTime(results.values)
    });
    AppleHealthKit.getBasalEnergyBurned(options, (error, results) => {
      if(error){
        console.log("error getting basal energy");
        return;
      }
      //setBasalEnergyBurned(results.values);
    });
    AppleHealthKit.getActiveEnergyBurned(options, (error, results) => {
      if(error){
        console.log("error getting active energy burned")
        return;
      }
      //setCaloriesBurned(results.values);
    })
  }, [hasPermissions])

  async function sendSteps(){
    axios.post("http://192.168.222.114:5082/api/Step", {
      DailySteps: steps,
      StartDate: startDate,
      EndDate: endDate
    }
    ).then((result) => {
      console.log(result);
    }).catch((err) => {
      console.log(err);
    })  
  }
  async function sendActiveEnergyBurned(){
    axios.post("http://192.168.222.114:5082/api/BurnedCalories", {
      Calories: steps,
      StartTime: startDate,
      EndTime: endDate
    },
    {
      headers: {
        "content-type": "application/x-www-form-urlencoded",
        accept: "application/json"
      }
    }
    ).then((result) => {
      console.log(result.data);
    }).catch((err) => {
      console.log(err);
    })  
  }

  async function sendBMR(){
    axios.post("http://192.168.222.114:5082/api/BMR", {
      Calories: basalEnergyBurned,
      StartTime: startDate,
      EndTime: endDate
    },
    {
      headers: {
        "content-type": "application/x-www-form-urlencoded",
        accept: "application/json"
      }
    }
    ).then((result) => {
      console.log(result);
    }).catch((err) => {
      console.log(err);
    })  
  }

  

  //Android-HealthConnect
 const readSampleData = async () => {
    const isInitialized = await initialize();
    if(!isInitialized){
      return;
    }
    await requestPermission([
      {
        accessType: 'read', 
        recordType: 'Steps'
      },
      {
        accessType: 'read',
        recordType: 'ActiveCaloriesBurned'
      },
      {
        accessType: 'read',
        recordType: 'BasalMetabolicRate'
      }
    ]);

    //setting dates
    var startDate = new Date(date.setHours(0,0,0,0)).toISOString();
    var endDate = new Date(date.setHours(23,59,59,59)).toISOString();

    //filter
    const timeRangeFilter: TimeRangeFilter = {
      operator: 'between',
      startTime: startDate,
      endTime: endDate
    };

    setStartDate(startDate.substring(0,10));
    setEndDate(endDate.substring(0,10));

    //Steps
    const steps = await readRecords('Steps', {timeRangeFilter});
    const totalSteps = steps.reduce((sum, cur) => sum + cur.count, 0);
    console.log("Steps: " + totalSteps)
    setSteps(totalSteps);

    //Calories
    const activeCalories = await readRecords('ActiveCaloriesBurned', {timeRangeFilter});
    const totalcalories = activeCalories.reduce((sum, cur) => sum + cur.energy.inKilocalories, 0);
    console.log("Calories: " + totalcalories)
    setCaloriesBurned(totalcalories);
    

    //Bmr
    const bmr = await readRecords('BasalMetabolicRate', {timeRangeFilter});
    const totalbmr = bmr.reduce((sum, cur) => sum + cur.basalMetabolicRate.inKilocaloriesPerDay, 0);
    console.log("Bmr: " + totalbmr);
    setBasalEnergyBurned(totalbmr);
    
 };

 useEffect(() => {
  if(Platform.OS != 'android'){
    return;
  }
    readSampleData();
  }, [])

  useEffect(() => {
    if(steps > 0){
      
      sendSteps()
    }
  },[steps])

  useEffect(() => {
    if(caloriesBurned > 0){
      sendActiveEnergyBurned()
    }
  }, [caloriesBurned])

  useEffect(() => {
    if(basalEnergyBurned > 0){
      sendBMR()
    }
  }, [basalEnergyBurned])

  return {
    steps,
    caloriesBurned,
    basalEnergyBurned
  };
};

export default useHealthData;

