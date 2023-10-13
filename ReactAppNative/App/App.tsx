import { StatusBar } from 'expo-status-bar';
import { useEffect, useState } from 'react';
import { StyleSheet, Text, View } from 'react-native';
import AppleHealthKit, {HealthInputOptions, HealthKitPermissions} from 'react-native-health';
import useHealthData from './src/hooks/useHealthData';

export default function App() {
  const {steps} = useHealthData(new Date(2023, 10, 13));

  return (
    <View style={styles.container}>
      <Text>Amount of steps:</Text>
      <Text>{steps}</Text>
      <StatusBar style="auto" />
    </View> 
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
