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
import servicioCategorias from "@/services/servicio-categorias"
import servicio from "@/services/servicio-publicacion"
export default {
    data() {
        return {
            categorias:[],
            idCatPublicaciones:0
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
            this.$loading(true)
			servicioCategorias.consultarCategorias(0)
				.then(r => {
                    this.$loading(false)
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
                let r = await this.$msg.question('Se dará de alta la publicación, ¿desea continuar?')
                if(r.value){
                    this.$loading(true)
                    let r = await servicio.guardarPublicacion(formData)
                    this.$loading(false)
                    if(r.value){
                        this.$msg.success(r.message);
                        this.$router.push({ path: "/usuarios" })
                        .catch(err => err)
                    }
                    else{
                        this.$msg.info(r.message);
                    }
                }
            } catch (error) {
                this.$loading(false)
                console.log('guardarPublicacion', error)
            }
            
        }
    }
}
</script>
<style scoped>

</style>