import logo from './logo.svg';
import './App.css';
import LoginButton from "./components/login";
import LogoutButton from "./components/logout";
import { useEffect } from 'react';
import { gapi } from 'gapi-script';

var SCOPES = 'https://www.googleapis.com/auth/fitness.activity.read';
const clientId = "704267478812-snaf5fajvh8j62b5d16u781q4c8c2imv.apps.googleusercontent.com"

function App() {
  console.log(process.env.REACT_APP_GOOGLE_SCOPES);
  console.log(process.env.REACT_APP_OTHER_VALUE);
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
    <div className="App">
      <LoginButton/>
      <LogoutButton/>
    </div>
  );
}

export default App;
