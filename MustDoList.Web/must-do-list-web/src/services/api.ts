import axios from "axios"
import { parseCookies } from "nookies";


export const apiBase = axios.create();


const {"mustdotoken-token": token} = parseCookies();

export const apiApi = axios.create({
    baseURL: process.env.API_URL
});

apiApi.interceptors.request.use(config => {
    console.debug(config);
    return config;
})

if(token) {
    apiApi.defaults.headers['Authorization'] = `Bearer ${token}`;
}