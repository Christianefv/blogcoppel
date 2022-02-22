<template>
    <div class="row">
        <div class="col-12 h4 text-center mt-4">
            Publicar nuevo tema
        </div>
        <pm-datos-publicacion   :categorias="categorias"
                                :idCatPublicaciones="idCatPublicaciones"
                                @guardarPublicacion="guardarPublicacion">
        </pm-datos-publicacion>
    </div>
</template>
<script>
import PmDatosPublicacion from "@/components/privada/DatosPublicacion"
import servicioInicio from "@/services/servicio-publico"
import servicio from "@/services/servicio-publicacion"
export default {
    data() {
        return {
            categorias:[],
            idCatPublicaciones:0
            // idCategoriaSeleccionada:0,
            // publicacion:{
            //     titulo:'',
            //     descripcion:''
            // },
            // file:[],
        }
    },
    components: {
        PmDatosPublicacion
	},
    mounted() {	
        this.consultarCategorias()
    },
    methods: {
        consultarCategorias(){
			servicioInicio.consultarCategorias(0)
				.then(r => {
					console.log(r)
					if(r.value){
						this.categorias = r.data
					}
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
		},
        loadFiles() {
            this.$refs.archivo.click();
        },
        handleFilesUpload() {
            this.file = this.$refs.archivo.files[0]
        },
        async guardarPublicacion(formData){
            try {
                
                let r = await servicio.guardarPublicacion(formData)
                if(r.value){
                    this.$msg.success(r.message);
                    this.$router.push({ path: "/usuarios" })
                    .catch(err => err)
                }
                else{
                    this.$msg.info(r.message);
                }
            } catch (error) {
                console.log('guardarPublicacion', error)
            }
            
        }
    }
}
</script>
<style scoped>

</style>