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

		if(window.localStorage.tokenWebUsuarios)
		{
			lstoken = JSON.parse(window.localStorage.tokenWebUsuarios)
			token = lstoken.token
            //console.log('token', token)
		}
        
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
		if(response.status == 202){
			return response;
		}
		else{
			Swal.fire({
				text: response.statusText,
				icon: 'warning'
			})
		}
	},
	error => {
		const { config, response} = error
		const originalRequest = config
		//console.log(originalRequest)
		if(response != null) {
			if (response.status === 401) {
				console.log("Unauthorized! :(")

				if (!isAlreadyFetchingAccessToken) {
					isAlreadyFetchingAccessToken = true

					const serv = axios.create({
						baseURL: configService.apiUrl,
					})

					let lstoken = JSON.parse(window.localStorage.tokenWebUsuarios)

					serv.post('/seguridad/renovar-token', {"refresh_token": lstoken.refreshToken })
						.then(response => {
							isAlreadyFetchingAccessToken = false
							
                            let token = response.data.data.accessToken
							console.log('/seguridad/renovar-token', token)
							
                            lstoken.expiresAt = response.data.expiresIn
							lstoken.refreshToken = response.data.refreshToken
							
                            originalRequest.headers.Authorization = `Bearer ${window.localStorage.tokenWebUsuarios}`
							axios(originalRequest)
						})
						.catch(error => {
							console.error(error)
						})
				}
			}
			else {
				Swal.fire({
					text: 'Ocurrio un error al procesar la petición del servicio.',
					icon: 'warning'
				})
				console.log("Ocurrio un error en el servicio => response.status == " + response.status, response)
				// Swal.fire({
				// 	text: 'La sesión ha caducado',
				// 	icon: 'warning'
				// })
			}
		}
		return Promise.reject(error)
	})

export default servicio