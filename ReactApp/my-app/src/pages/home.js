import LoginButton from "../components/login";
import LogoutButton from "../components/logout";
import Dashboard from './dashboard';
import { useEffect } from 'react';
import { gapi } from 'gapi-script';

var SCOPES = "https://www.googleapis.com/auth/fitness.activity.read https://www.googleapis.com/auth/fitness.body.read https://www.googleapis.com/auth/fitness.reproductive_health.read"
const clientId = "704267478812-snaf5fajvh8j62b5d16u781q4c8c2imv.apps.googleusercontent.com"
function Home(){
    useEffect(() => {
        function start(){
          gapi.client.init({
            clientId: clientId,
            scope: SCOPES
          })
        };
        gapi.load('client:auth2', start);
      });
      return (
        <div>
          <LoginButton/>
          <LogoutButton/>
        </div>
      );
}

export default Home;