import base from '@/services/servicio-base';

const servicio = {};

servicio.guardarCategorias = function(formData) {
    return base.post('/categorias/v1', formData,
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
export default servicio;