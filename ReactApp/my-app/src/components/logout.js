import { GoogleLogout } from 'react-google-login';

const clientId = "704267478812-snaf5fajvh8j62b5d16u781q4c8c2imv.apps.googleusercontent.com"

function Logout(){
    const onSuccess = () => {
        console.log("Log out successfull!");
    }

    return(
        <div id="signOutButton">
            <GoogleLogout
                clientId={clientId}
                buttonText={"Logout"}
                onLogoutSuccess={onSuccess}
            />
        </div>
    )
}

export default Logout;