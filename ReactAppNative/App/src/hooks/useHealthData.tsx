import { useEffect, useState } from 'react';
import AppleHealthKit, {HealthInputOptions, HealthKitPermissions, HealthValue} from 'react-native-health';
import { Platform } from 'react-native';
import { initialize, readRecords, requestPermission } from 'react-native-health-connect';
import { TimeRangeFilter } from 'react-native-health-connect/lib/typescript/types/base.types';

const permissions: HealthKitPermissions = {
    permissions: {
        read: [AppleHealthKit.Constants.Permissions.Steps, AppleHealthKit.Constants.Permissions.BasalEnergyBurned, AppleHealthKit.Constants.Permissions.ActiveEnergyBurned, AppleHealthKit.Constants.Permissions.AppleExerciseTime],
        write: []
    }
  }

const useHealthData = (date: Date) => {
  const [hasPermissions, setHasPermissions] = useState(false);
  const [steps, setSteps] = useState(0);
  const [exerciseTime, setExerciseTime] = useState({});
  const [basalEnergyBurned, setBasalEnergyBurned] = useState({});
  const [caloriesBurned, setCaloriesBurned] = useState(0);
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("")


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
    console.log(steps)
    console.log(startDate)
    console.log(endDate)
    fetch('https://192.168.222.114:7212/api/Step', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        DailySteps: steps,
        StartDate: startDate,
        EndDate: endDate,
       //UserEmail: user.email
      })
    }).then((response) => {
      console.log(response)
    }).catch((error) => {
      console.log(error)
    });
  }

  /*async function sendExerciseTime(){
    fetch('https://localhost:7212/api/MoveMinutes', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        MoveMinutes: exerciseTime,
        StartTime: startTime,
        EndTime: endTime,
        UserEmail: user.email
      }),
    });
  }*/

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
        recordType: 'TotalCaloriesBurned'
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

    setStartDate(startDate.toString());
    setEndDate(endDate.toString());

    //Steps
    const steps = await readRecords('Steps', {timeRangeFilter});
    const totalSteps = steps.reduce((sum, cur) => sum + cur.count, 0);
    console.log("Steps: " + totalSteps)
    setSteps(totalSteps);

    //Calories
    const activeCalories = await readRecords('TotalCaloriesBurned', {timeRangeFilter});
    const totalcalories = activeCalories.reduce((sum, cur) => sum + cur.energy.inKilocalories, 0);
    setCaloriesBurned(totalcalories);
    console.log("Calories: " + totalcalories)

    //Bmr
    const bmr = await readRecords('BasalMetabolicRate', {timeRangeFilter});
    console.log("Bmr: " + bmr);
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

  return {
    steps,
    caloriesBurned
  };
};

export default useHealthData;

