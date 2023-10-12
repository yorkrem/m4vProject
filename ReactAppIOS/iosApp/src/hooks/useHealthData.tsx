import { useEffect, useState } from 'react';
import AppleHealthKit, {HealthInputOptions, HealthKitPermissions} from 'react-native-health';

const permissions: HealthKitPermissions = {
    permissions: {
        read: [AppleHealthKit.Constants.Permissions.Steps],
        write: []
    }
  }

const useHealthData = (date: Date) => {
  const [hasPermissions, setHasPermissions] = useState(false);
  const [steps, setSteps] = useState(0);

  useEffect(() => {
    AppleHealthKit.initHealthKit(permissions, (error) => {
      if(error){
        console.log(error);
        return;
      }
      setHasPermissions(true);
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

  return {
    steps
  };
};

export default useHealthData;

