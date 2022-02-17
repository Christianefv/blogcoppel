import base from '@/services/servicio-base'

const servicio  = {}

servicio.altaUsuario = (usuario) => {
    return base.post('/usuario/alta', usuario)
            .then(r => {
                return r.data
            })
            .catch(err => {
                console.log(err)
            })
}

export default servicio