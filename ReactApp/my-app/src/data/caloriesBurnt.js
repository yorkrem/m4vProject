import axios from "axios"

export async function burnedCaloriesRequest(token, startTime, endTime, user){
    await axios.post("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate", 
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
      if(response.data.bucket[0].dataset[0].point && response.data.bucket[0].dataset[0].point.length > 0){
        console.log("saving calories")
        saveBurnedCalories(response.data.bucket[0].dataset[0].point[0].value[0].fpVal, startTime, endTime, user)
      }
    });
}

async function saveBurnedCalories(burnedCalories, startTime, endTime, user){
    await axios.post('https://localhost:7212/api/BurnedCalories', {
        Calories: burnedCalories,
        StartTime: startTime,
        EndTime: endTime,
        UserEmail: user.email
      })
      .catch(function (error) {
        console.log(error);
      });
}