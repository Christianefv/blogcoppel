<template>
    <div class="row col m-4">
        <div class="col-12">
            <div class="form-group row">
                <label  class="col-3 col-form-label text-sm-right pr-0">
                    <b>Categoría:</b></label>            
                <div class="col-8">
                    <select class="form-control form-control-sm input-select"
                        v-model="idCategoriaSeleccionada">
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
                    <label class="col-3 col-form-label text-sm-right pr-0">Adjuntar Imagen:</label>
                    <div class="col-2 col-lg-1">
                        <div class="input-group-append">
                            <div class="input-group-text form-control-sm inputFilesAppendPrepend">
                                <i href="#" @click="loadFiles" class="far fa-image"></i>
                            </div>
                        </div>
                        <input type="file" id="archivo" ref="archivo" v-on:change="handleFilesUpload" accept=".jpeg, .jpg" style="display: none;" />
                    </div>
                    <div div class="input-group-append col-6 col-lg-7">
                        <span v-if="file.length==0" class="form-control-sm">* Solo archivos JPG</span>
                    </div>
                </div>
                <!-- modal vista previa -->
                 <b-modal id="modal-vista-previa" size="xl" title="Vista previa" hide-footer>
                    <div class="d-flex" >
                        <div>                            
                            <img    v-if="url" class="img-fluid card-categoria d-none d-md-none d-lg-block" 
                                    :src="url"
                                    alt="Responsive image"/>
                        </div>
                        <div class="ml-auto w-100 p-3">
                            <div class="font-weight-bold font-poppins">
                                {{publicacion.titulo}}
                            </div>
                            <div class="font-weight-bolder ml-2">
                                <textarea v-model="publicacion.descripcion" class="form-control" disabled></textarea></div>
                        </div>
                    </div>            
                    <div class="col d-flex justify-content-end  text-rigth">
                        <span> Autor: {{usuarioGlobal().usuario}}</span>
                    </div> 
                    <b-button class="mt-3" block @click="$bvModal.hide('modal-vista-previa')">Cerrar</b-button>
                </b-modal> 
                <div class="pt-3 form-group row">

                    <div class="col-11 d-flex justify-content-end">
                        <button class=" btn btn-info mr-1"
                            @click="vistaPrevia">Vista previa</button>
                        <button class=" btn btn-primary"
                            @click="guardarPublicacion">Guardar</button>
                    </div>
                </div>
        </div>
    </div>
</template>
<script>
export default {
    props:["categorias", "idCatPublicaciones"],
    data() {
        return {            
            idCategoriaSeleccionada:0,
            publicacion:{
                titulo:'',
                descripcion:''
            },
            file:[],
            url: null,
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
        handleFilesUpload() {
            this.file = this.$refs.archivo.files[0]
            this.url = URL.createObjectURL(this.file)
            //console.log('file file ',  this.file.path )
        },
        async guardarPublicacion(){
            try {
                if(this.idCategoriaSeleccionada==0){
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
                formData.append('idCatPublicaciones', this.idCatPublicaciones)         
                formData.append('idCatCategorias', this.idCategoriaSeleccionada)
                formData.append('idCatUsuarios', this.usuarioGlobal().idCatUsuario)
                formData.append('titulo', this.publicacion.titulo)
                formData.append('descripcion', this.publicacion.descripcion)
                formData.append('file', this.file);

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