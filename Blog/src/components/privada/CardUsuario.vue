<template>
    <div class="row">
        <div    :class="usuario.avatar=='' ? 'd-none':'d-none col-md-4 d-md-block align-items-center'">
            <img    class="img-fluid d-none d-md-block d-lg-block" 
                    v-bind:src="'data:image/jpeg;base64,' + usuario.avatar" 
                    alt="Responsive image"/>
        </div>
        <div class="col-12 col-md-8">
            <div class="row">
                <div class="form-group col-12 col-lg-6">
                    <label>Nombre del contacto</label>
                    <label class="form-control">{{usuario.nombre}}</label>
                </div>        
                <div class="form-group col-12 col-lg-6">
                    <label>Correo electr&oacute;nico</label>
                    <label class="form-control">{{usuario.correo}}</label>
                </div>
                <div class="form-group col-12 col-lg-6">
                    <label>Usuario</label>
                    <label class="form-control">{{usuario.usuario}}</label>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ["usuario"],
    
}
</script>