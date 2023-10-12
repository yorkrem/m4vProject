import { useEffect, useState } from 'react';
import AppleHealthKit, {HealthInputOptions, HealthKitPermissions} from 'react-native-health';
import { Platform } from 'react-native';

const permissions: HealthKitPermissions = {
    permissions: {
        read: [AppleHealthKit.Constants.Permissions.Steps, AppleHealthKit.Constants.Permissions.BasalEnergyBurned, AppleHealthKit.Constants.Permissions.ActiveEnergyBurned, ],
        write: []
    }
  }

const useHealthData = (date: Date) => {
  const [hasPermissions, setHasPermissions] = useState(false);
  const [steps, setSteps] = useState(0);

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

  useEffect(() => {
    if(Platform.OS != 'android'){
      return;
    }
  })

  return {
    steps
  };
};

export default useHealthData;

