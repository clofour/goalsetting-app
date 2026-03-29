import axios from 'axios';

const api = axios.create({
    baseURL: "/api",
    headers: {
        "Content-Type": "application/json"
    }
})

export const signIn = function() {

}

export const getGoals = function() {
    
}