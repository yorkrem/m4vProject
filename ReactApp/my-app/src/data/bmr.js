import axios from "axios";

export async function bmrRequest(token, startTime, endTime, user){
    await axios.post("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate", 
    {
        "aggregateBy": [{
            "dataTypeName": "com.google.calories.bmr",
            "dataSourceId": "derived:com.google.calories.bmr:com.google.android.gms:merged"
          }],
          "bucketByTime": { "durationMillis": 86400000 },
          "startTimeMillis": startTime,
          "endTimeMillis": endTime
    },{
        headers: { Authorization: 'Bearer ' + token }
    })
    .then(function (response) {
      console.log(response)
      //saveSteps(response.data?.bucket[0]?.dataset[0]?.point[0]?.value[0]?, startTime, endTime, user)
    });
    
}

async function saveSteps(bmr, startTime, endTime, user){
    await axios.post('https://localhost:7212/api/BMR', {
        Bmr: bmr,
        EndTime: endTime,
        UserEmail: user.email
        })
        .catch(function (error) {
        console.log(error);
        });
}