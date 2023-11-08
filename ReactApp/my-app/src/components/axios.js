import axios from "axios";

const instance = axios.create({
    baseURL: "http://localhost:5082/api",
});

export default instance;