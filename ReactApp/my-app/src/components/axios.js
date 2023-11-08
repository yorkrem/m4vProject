import axios from "axios";
import { useState, useEffect } from "react";



const instance = axios.create({
    baseURL: "https://localhost:7212/api",
});

export default instance;