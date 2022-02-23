<template>
    <div class="row">        
		<pm-header class="col-12"></pm-header>
        <div class="col-12 h4 text-center mt-4">
            Últimas publicaciónes
        </div>
        <div 	class="col-12 mt-4" 
				:key="index" 
				v-for="(publicacion, index) in publicaciones">
            <card-publicacion 
				:publicacion="publicacion"
				:motoTarjeta="modoTarjeta"
				:index="index"
                @seleccionarPublicacion="seleccionarPublicacion">
			</card-publicacion> 
		</div>
        <div class="col-12 d-flex justify-content-end" v-if="modoTarjeta=='true'">
            <button class="btn btn-outline-primary m-1" 
                    title="Comentar"
                    @click="comentar">
                <i 	class="fas fa-comment"></i>
            </button>
        </div>
    </div>
</template>
<script>
import PmHeader from "@/components/publica/Header"
import servicio from "@/services/servicio-publico"
import CardPublicacion from "@/components/CardPublicacion"
export default({
    data() {
        return {
            idCatCategoria: 0,
            publicaciones:[],
            modoTarjeta:false,
            idUsuario:0,
            idPublicacion:0
        }
    },
    components: {
		CardPublicacion,
		PmHeader
	},
    computed:{
    },
    mounted() {
        
        this.idUsuario = this.$route.params.idCatUsuarios
        this.idCatCategoria = this.$route.params.idCatCategorias
        this.idPublicacion = this.$route.params.idCatPublicaciones
        this.modoTarjeta = this.$route.params.modoTarjeta
        this.consultarPublicaciones(this.idUsuario,this.idCatCategoria, this.idPublicacion)
    },
    methods: {
        consultarPublicaciones( idUsuario, idCatCategoria, idPublicacion){
			this.$loading(true)
			servicio.consultarPublicaciones( idUsuario, idCatCategoria, idPublicacion)
				.then(r => {
					console.log(r.data)
					if(r.value){
						this.$loading(false)
						this.publicaciones = r.data
					}
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
		},
        seleccionarPublicacion(publicacion){
            console.log(publicacion)
            // this.$router.push({ name: 'Posts', 
            // params: {   idCatCategorias: publicacion.idCatCategorias
            //             ,idCatUsuarios: publicacion.idCatUsuarios
            //             ,idCatPublicaciones: publicacion.idCatPublicaciones 
            //             ,motoTarjeta: true  } })
            // this.$router.replace({ path: `/Posts/${publicacion.idCatCategorias}/${publicacion.idCatUsuarios}/${publicacion.idCatPublicaciones }/true` })
            this.$router
				.push({ path: `/Posts/${publicacion.idCatUsuarios}/${publicacion.idCatCategorias}/${publicacion.idCatPublicaciones }/true` })
				.catch(err => err)
                this.$router.go()
        },
        comentar(){
            this.$router
				.push({ path: `/Login` })
				.catch(err => err)
        }
    }
})
</script>
<style scoped>
</style>