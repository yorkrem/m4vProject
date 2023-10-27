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
  const [activeEnergyBurned, setActiveEnergyBurned] = useState({});


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
      setExerciseTime(results.values)
    });
    AppleHealthKit.getBasalEnergyBurned(options, (error, results) => {
      if(error){
        console.log("error getting basal energy");
        return;
      }
      setBasalEnergyBurned(results.values);
    });
    AppleHealthKit.getActiveEnergyBurned(options, (error, results) => {
      if(error){
        console.log("error getting active energy burned")
        return;
      }
      setActiveEnergyBurned(results.values);
    })
  }, [hasPermissions])

  async function sendSteps(){
    fetch('https://localhost:7212/api/Step', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        DailySteps: steps,
        /*StartTime: startTime,
        EndTime: endTime,
        UserEmail: user.email*/
      }),
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
      {accessType: 'read', recordType: 'Steps'},
    ]);

    const timeRangeFilter: TimeRangeFilter = {
      operator: 'between',
      startTime: new Date(date.setHours(0,0,0,0)).toISOString(),
      endTime: new Date(date.setHours(23, 59, 59,59)).toISOString()
    };
    
    //Steps
    const steps = await readRecords('Steps', {timeRangeFilter});
    console.log(steps);
    const totalSteps = steps.reduce((sum, cur) => sum + cur.count, 0);
    setSteps(totalSteps);
 };

 useEffect(() => {
  if(Platform.OS != 'android'){
    return;
  }
  readSampleData();
})



  return {
    steps
  };
};

export default useHealthData;

