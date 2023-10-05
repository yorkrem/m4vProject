import axios from "axios"

export async function moveMinutesRequest(token, startTime, endTime, user){
    await axios.post("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate", 
    {
        "aggregateBy": [{
            "dataTypeName": "com.google.active_minutes",
            "dataSourceId": "derived:com.google.active_minutes:com.google.android.gms:merge_active_minutes"
          }],
          "bucketByTime": { "durationMillis": 86400000 },
          "startTimeMillis": startTime,
          "endTimeMillis": endTime
    },{
        headers: { Authorization: 'Bearer ' + token }
    })
    .then(function (response) {
      saveMoveMinutes(response.data?.bucket[0]?.dataset[0]?.point[0]?.value[0]?.intVal, startTime, endTime, user)
    });
}

async function saveMoveMinutes(moveMinutes, startTime, endTime, user){
    await axios.post('https://localhost:7212/api/MoveMinutes', {
        MoveMinutes: moveMinutes,
        StartTime: startTime,
        EndTime: endTime,
        UserEmail: user.email
      })
      .catch(function (error) {
        console.log(error);
      });
}