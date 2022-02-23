<template>
    <div class="d-block text-center">
       <div class="col-12 h4 text-center mt-4">
            Publicar nuevo tema
        </div>
        <div class="form-group row">
            <label  for="txtCategoria" 
                    class="col-3 col-form-label text-sm-right pr-0"><b>Titulo:</b></label>
            <div class="col-8">
                <input  type="text" 
                        class="form-control form-control-sm" 
                        v-model.trim="categoria.titulo" id="txtCategoria" 
                        ref="txtNombre" tabindex="1" placeholder="Ej. Deportes">
            </div>
        </div>
        <div class="form-group row">
            <label  for="txtCategoria" 
                    class="col-3 col-form-label text-sm-right pr-0"><b>Descripcion:</b></label>
            <div class="col-8">
                <textarea   v-model="categoria.descripcion" 
                            class="form-control form-control-sm" 
                            style="height:300px"
                            placeholder="Descripción del contenido de la categoría"></textarea>
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
        <b-modal id="modal-vista-previa-categorias" size="xl" title="Vista previa" hide-footer>
            <div class="d-flex" >
                <div>
                    <img    v-if="url" class="img-fluid card-categoria d-none d-md-none d-lg-block" 
                            :src="url"
                            alt="Responsive image"/>
                </div>
                <div class="ml-auto w-100 p-3">
                    <div class="font-weight-bold font-poppins">
                        {{categoria.titulo}}
                    </div>
                    <div class="font-weight-bolder ml-2">
                        <textarea v-model="categoria.descripcion"  style="height:300px" class="form-control" disabled></textarea></div>
                </div>
            </div>
            <b-button class="mt-3" block @click="$bvModal.hide('modal-vista-previa-categorias')">Cerrar</b-button>
        </b-modal> 
        <div class="pt-3 form-group row">
            
            <div class="col-11 d-flex justify-content-end">
                <button class=" btn btn-info mr-1"
                    @click="vistaPrevia">Vista previa</button>
                <button class=" btn btn-primary"
                    @click="guardarCategoria"
                    >Guardar</button>
            </div>
        </div>
    </div>
</template>
<script>
import servicio from "@/services/servicio-categorias"
export default({
    data() {
		return {
			categoria:{
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
    methods:{
        loadFiles() {
            console.log('click')
            this.$refs.archivo.click();
        },
        handleFilesUpload() {
            this.file = this.$refs.archivo.files[0]
            this.url = URL.createObjectURL(this.file)
        },
        async guardarCategoria(){
            try {
                let r = await this.$msg.question('Se dará de alta la categoría, ¿desea continuar?')
                if(r.value){
                    let formData = new FormData()
                    formData.append('titulo', this.categoria.titulo)
                    formData.append('descripcion', this.categoria.descripcion)
                    formData.append('file', this.file);
                    this.$loading(true)
                    servicio.guardarCategorias(formData)
                    .then(r => {
                        if(r.value){
                            this.$msg.success(r.message)
                            this.$emit('togleMenu', 5)
                            this.$router.push({ path: "/mis-categorias" })
                            .catch(err => err)
                        }
                        this.$loading(false)
                    })
                    .catch(e => {
                        console.log(e)
                        this.$loading(false)
                    })
                }
                
            } catch (error) {
                this.$loading(false)
                console.log('guardarCategoria', error)
            }
            
        },
        vistaPrevia(){
            this.$bvModal.show('modal-vista-previa-categorias')
        }
    }
})
</script>
<style scoped>
.inputFilesAppendPrepend, .inputFilesAppendPrepend > a {
    border: 0px;
    border-radius: 0px;
    color: blue !important;
    font-weight: bold;
    background-color: white;
}

.card-categoria {
    width: 318px;
    height: 317px;
}
</style>