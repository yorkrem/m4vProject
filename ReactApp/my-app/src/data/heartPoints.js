import axios from "axios";

export async function heartPointsRequest(token, startTime, endTime, user){
    await axios.post("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate", 
    {
        "aggregateBy": [{
            "dataTypeName": "com.google.heart_minutes",
            "dataSourceId": "derived:com.google.heart_minutes:com.google.android.gms:merge_heart_minutes"
          }],
          "bucketByTime": { "durationMillis": 86400000 },
          "startTimeMillis": startTime,
          "endTimeMillis": endTime
    },{
        headers: { Authorization: 'Bearer ' + token }
    })
    .then(function (response) {
        if(response.data.bucket[0].dataset[0].point && response.data.bucket[0].dataset[0].point.length > 0){
            console.log("saving heart points")
            saveHeartPoints(response.data.bucket[0].dataset[0].point[0].value[0].fpVal, startTime, endTime, user)
        }
    });
    
}

async function saveHeartPoints(heartPoints, startTime, endTime, user){
    await axios.post('https://localhost:7212/api/HeartPoint', {
        HeartPoints: heartPoints,
        StartTime: startTime,
        EndTime: endTime,
        UserEmail: user.email
        })
        .catch(function (error) {
        console.log(error);
        });
}