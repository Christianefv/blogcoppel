import base from '@/services/servicio-base'

const servicio  = {}

servicio.login = (usuario) => {
    return base.post('/seguridad/login', usuario)
            .then(r => {
                return r.data
            })
            .catch(err => {
                console.log(err)
            })
}

export default servicio