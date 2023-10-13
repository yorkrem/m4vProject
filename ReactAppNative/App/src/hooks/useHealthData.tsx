import { useEffect, useState } from 'react';
import AppleHealthKit, {HealthInputOptions, HealthKitPermissions} from 'react-native-health';
import { Platform } from 'react-native';
import { initialize, readRecords, requestPermission } from 'react-native-health-connect';
import { TimeRangeFilter } from 'react-native-health-connect/lib/typescript/types/base.types';

const permissions: HealthKitPermissions = {
    permissions: {
        read: [AppleHealthKit.Constants.Permissions.Steps, AppleHealthKit.Constants.Permissions.BasalEnergyBurned, AppleHealthKit.Constants.Permissions.ActiveEnergyBurned, ],
        write: []
    }
  }

const useHealthData = (date: Date) => {
  const [hasPermissions, setHasPermissions] = useState(false);
  const [steps, setSteps] = useState(0);


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
      console.log(results.value);
      setSteps(results.value);
    })
  }, [hasPermissions])

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

