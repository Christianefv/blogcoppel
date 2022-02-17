import axios from 'axios'
import configService from './config'
import Swal from 'sweetalert2'

let isAlreadyFetchingAccessToken = false

const servicio = axios.create({
    baseURL: configService.apiUrl,
})

servicio.interceptors.request.use(
    config => {
        let lstoken = null;
        let token = "";
        if (window.localStorage.tokenWebUsuarios) {
            lstoken = JSON.parse(window.localStorage.tokenWebUsuarios)
            token = lstoken.token
        }
        //let token = window.localStorage.token
        if (token)
            config.headers.authorization = `Bearer ${token}`

        return config
    },
    error => {
        Swal.fire({
            text: 'Ocurrio un error al realizar la peticion al servidor',
            icon: 'warning'
        })
        return Promise.reject(error)
    }
)

servicio.interceptors.response.use(
    response => {
        console.log('response ', response)
        return response;
    },
    error => {
        console.log("error-51", error)

        const { config, response } = error
        const originalRequest = config
            //console.log(originalRequest)
        if (response != null) {
            if (response.status === 401) {
                //console.log("Unauthorized! :(")

                if (!isAlreadyFetchingAccessToken) {
                    isAlreadyFetchingAccessToken = true

                    const serv = axios.create({
                        baseURL: configService.apiUrl,
                    })

                    serv.post('/seguridad/renovar-token', { "refresh_token": window.localStorage.refreshToken })
                        .then(response => {
                            isAlreadyFetchingAccessToken = false

                            let token = response.data.accessToken

                            let lstoken = JSON.parse(window.localStorage.tokenWebUsuarios)
                            lstoken.token = token
                            window.localStorage.expiresIn = response.data.expiresIn
                            window.localStorage.refreshToken = response.data.refreshToken
                            lstoken.user = window.atob(
                                token.split(".")[1]
                            )

                            window.localStorage.tokenWebUsuarios = lstoken;


                            originalRequest.headers.Authorization = `Bearer ${token}`
                            axios(originalRequest)
                        })
                        .catch(error => {
                            console.error(error)
                        })
                }
            } else {
                Swal.fire({
                    text: 'Ocurrio un error al realizar la peticion al servidor',
                    icon: 'warning'
                })
            }
        }
        return Promise.reject(error)
    })

export default servicio