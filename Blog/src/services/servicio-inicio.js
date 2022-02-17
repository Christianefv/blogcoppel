import base from '@/services/servicio-base';

const servicioInicio = {};

servicioInicio.consultarCategorias = function(idCategoria) {
    return base.get('/inicio/categorias/v1/' + idCategoria)
        .then(r => {
            return r.data;
        })
        .catch(e => {
            return e;
        })
}
export default servicioInicio;