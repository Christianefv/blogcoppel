import base from '@/services/servicio-base'

const servicio  = {}

servicio.altaUsuario = (usuario) => {
    return base.post('/usuarios/v1', usuario)
            .then(r => {
                return r.data
            })
            .catch(err => {
                console.log(err)
            })
}

servicio.consultarDatosUsuario = (idCatUsuario) => {
    return base.get('/usuarios/v1/' + idCatUsuario)
            .then(r => {
                return r.data
            })
            .catch(err => {
                console.log(err)
            })
}

servicio.enviarCorreo = (correo) => {
    return base.post('/usuarios/v1/enviar-correo/' + correo)
        .then(r => {
            return r.data
        })
        .catch(err => {
            console.log(err)
        })
}

servicio.validarCodigo = (correo, codigo) => {
    return base.get('/usuarios/v1/validar-codigo-correo/' + correo + '/' + codigo)
        .then(r => {
            return r.data
        })
        .catch(err => {
            console.log(err)
        })
}

export default servicio