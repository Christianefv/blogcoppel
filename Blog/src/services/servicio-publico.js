import base from '@/services/servicio-base';

const servicioInicio = {};

servicioInicio.consultarCategorias = function(idCategoria) {
    return base.get('/publico/categorias/v1/' + idCategoria)
        .then(r => {
            return r.data;
        })
        .catch(e => {
            return e;
        })
}

servicioInicio.consultarPublicaciones = function(idCatUsuarios, idCatCategorias, idCatPublicaciones) {
    return base.get('/usuarios/publicaciones/v1/' + idCatUsuarios + '/'+ idCatCategorias + '/' + idCatPublicaciones)
    .then(r => {
        return r.data;
    })
    .catch(e => {
        return e;
    })
}
export default servicioInicio;