import base from '@/services/servicio-base';

const servicio = {};

servicio.guardarPublicacion = function(formData) {
    return base.post('/usuarios/publicaciones/v1/', formData,
    {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
    .then(r => {
        return r.data;
    })
    .catch(e => {
        return e;
    })
}

servicio.consultarPublicaciones = function(idCatUsuarios, idCatCategorias, idCatPublicaciones) {
    return base.get('/usuarios/publicaciones/v1/' + idCatUsuarios + '/'+ idCatCategorias + '/' + idCatPublicaciones)
    .then(r => {
        return r.data;
    })
    .catch(e => {
        return e;
    })
}

servicio.eliminarrPublicaciones = function(idCatPublicaciones) {
    return base.delete('/usuarios/publicaciones/v1/' + idCatPublicaciones)
    .then(r => {
        return r.data;
    })
    .catch(e => {
        return e;
    })
}

servicio.modificarPublicaciones = function(formData) {
    return base.put('/usuarios/publicaciones/v1/', formData,
    {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
    .then(r => {
        return r.data;
    })
    .catch(e => {
        return e;
    })
}

servicio.consultarComentarios = function(idCatPublicaciones) {
    
    return base.get('/usuarios/publicaciones/comentarios/v1/' + idCatPublicaciones)
    .then(r => {
        return r.data;
    })
    .catch(e => {
        return e;
    })
}

servicio.guardarComentarios = function(comentario) {
    
    return base.post('/usuarios/publicaciones/comentarios/v1', comentario)
    .then(r => {
        return r.data;
    })
    .catch(e => {
        return e;
    })
}
export default servicio;