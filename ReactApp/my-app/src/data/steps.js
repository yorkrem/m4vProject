import axios from "axios"

export async function stepsRequest(token, startTime, endTime, user){
    await axios.post("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate", 
    {
        "aggregateBy": [{
            "dataTypeName": "com.google.step_count.delta",
            "dataSourceId": "derived:com.google.step_count.delta:com.google.android.gms:estimated_steps"
          }],
          "bucketByTime": { "durationMillis": 86400000 },
          "startTimeMillis": startTime,
          "endTimeMillis": endTime
    },{
        headers: { Authorization: 'Bearer ' + token }
    })
    .then(function (response) {
      saveSteps(response.data?.bucket[0]?.dataset[0]?.point[0]?.value[0]?.intVal, startTime, endTime, user)
    });
    
}

async function saveSteps(steps, startTime, endTime, user){
    await axios.post('https://localhost:7212/api/Step', {
        DailySteps: steps,
        StartTime: startTime,
        EndTime: endTime,
        UserEmail: user.email
        })
        .catch(function (error) {
        console.log(error);
        });
}