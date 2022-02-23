<template>
    <div class="row col m-4">
        <div class="col-12">
            <div class="form-group row">
                <label  class="col-3 col-form-label text-sm-right pr-0">
                    <b>Categoría:</b></label>            
                <div class="col-8">
                    <select class="form-control form-control-sm input-select"
                        v-model="publicacion.idCatCategorias">
                        <option :value="0" disabled>Seleccione...</option>
                        <option
                            :key="index"
                            :value="c.idCatCategorias"
                            v-for="(c, index) in categorias"
                        >{{ c.titulo }}</option>
                    </select>
                </div>
            </div>
        </div>
        <div class="col d-block text-center m-2">
                <div class="form-group row">
                    <label  for="txtpublicacion" 
                            class="col-3 col-form-label text-sm-right pr-0"><b>Titulo:</b></label>
                    <div class="col-8">
                        <input  type="text" 
                                class="form-control form-control-sm" 
                                v-model.trim="publicacion.titulo" 
                                id="txtpublicacion" 
                                ref="txtNombre" 
                                tabindex="1" 
                                placeholder="Título de la publicación">
                    </div>
                </div>
                <div class="form-group row">
                    <label  for="txtpublicacion" 
                            class="col-3 col-form-label text-sm-right pr-0"><b>Descripcion:</b></label>
                    <div class="col-8">
                        <textarea   v-model="publicacion.descripcion" 
                                    class="form-control form-control-sm" 
                                    style="height:300px"
                                    placeholder="Descripción del contenido de la publicación"></textarea>
                      </div>
                </div>
                <div class="form-group row">
                    <div class="col-11 d-flex justify-content-end">                        
                        <button class="btn btn-secondary m-2" 
                        @click="$bvModal.hide('modal-editar')">Cancelar</button>
                        <button class=" btn btn-primary m-2"
                            @click="guardarPublicacion">Guardar</button>
                    </div>
                </div>
        </div>
    </div>
</template>
<script>
export default {
    props:["categorias", "publicacion"],
    data() {
        return {
        }
    },
    components: {
	},
    mounted() { 
    },
    methods: {        
        loadFiles() {
            this.$refs.archivo.click();
        },
        async guardarPublicacion(){
            try {
                if(this.publicacion.idCategoriaSeleccionada==0){
                    this.$msg.info('Seleccionar la categoría a la que pertenece la publicación.')
                    return
                }
                if(this.publicacion.titulo==''){
                    this.$msg.info('Debe de agregar título al artículo que desea publicar')
                    return
                }
                 if(this.publicacion.descripcion==''){
                    this.$msg.info('Debe de agregar descripción al artículo que desea publicar')
                    return
                }

                let formData = new FormData()
                formData.append('idCatPublicaciones', this.publicacion.idCatPublicaciones)         
                formData.append('idCatCategorias', this.publicacion.idCatCategorias)
                formData.append('idCatUsuarios', this.usuarioGlobal().idCatUsuario)
                formData.append('titulo', this.publicacion.titulo)
                formData.append('descripcion', this.publicacion.descripcion)

                this.$emit("guardarPublicacion", formData)
            } catch (error) {
                console.log('guardarPublicacion', error)
            }
            
        },
        vistaPrevia(){
            this.$bvModal.show('modal-vista-previa')
        }
        
    }
}
</script>
<style scoped>

</style>