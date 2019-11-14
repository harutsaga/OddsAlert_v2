import _ from 'lodash'

export default class AuthService {

    constructor(domain) {
        this.domain = domain || '' // API server domain
    }

    login = async (username, password) => {

        // Get a token from api server using the fetch api
        let res = await this.fetch(`/api/auth`, {
            method: 'POST',
            body: JSON.stringify({
                username,
                password
            })
        })

        
        if(!_.isNil(res.token)){
            this.setToken(res.token) // Setting the token in localStorage
        }
        
        return res
    }

    register = async (data) => {
        let res = await this.fetch(`/api/register`, {
            method: 'POST',
            body: data
        })
        return res
    }

    loggedIn() {
        // Checks if there is a saved token and it's still valid
        const token = this.getToken() // Getting token from localstorage
        return !!token
    }

    setToken(idToken) {
        // Saves user token to localStorage
        window.localStorage.setItem('token', idToken)
    }

    getToken() {
        // Retrieves the user token from localStorage
        return window.localStorage.getItem('token')
    }

    logout() {
        // Clear user token and profile data from localStorage
        window.localStorage.removeItem('token')
    }

    fetch = async (url, options) => {
        // performs api calls sending the required authentication headers
        const headers = {
            'Content-Type': 'application/json'
        }

        // Setting Authorization header
        if (this.loggedIn()) {
            headers['Authorization'] = 'Token ' + this.getToken()
        }

        try{
            let response = await fetch(url, {
                headers,
                timeout: 500,
                ...options
            }).catch(error => console.log(error))
            return response.json()
        }
        catch(err)
        {
            return []
        }
    }
}
