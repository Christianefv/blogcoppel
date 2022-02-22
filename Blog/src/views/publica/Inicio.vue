<template>
    <div class="row">
		<pm-header class="col-12"></pm-header>
        <!-- Introduccion -->
		<div class="col-12">
			<img src="@/assets/Blog.jpg" alt="image slot" class="tamanoimagen w-100">
		</div>
		<div class="col-12 m-3 h5 text-center">
			Categorías
			<div 	class="col-12 p-0 m-0 py-4" 
					:key="index" 
					v-for="(categoria, index) in categorias">
				<pm-card-categoria class="col-12"
					:categoria="categoria"
					:index="index"
					@categoriaSeleccionada="categoriaSeleccionada">
				</pm-card-categoria>
			</div>
		</div>
    </div>
</template>
<script>
import PmHeader from "@/components/publica/Header"
import servicio from "@/services/servicio-publico"
import PmCardCategoria from "@/components/CardCategoria"
export default {
	data() {
		return {
			categorias:[]
		}
	},	
	components: {
		PmCardCategoria,
		PmHeader
	},
	mounted() {
		this.consultarCategorias()
	},
	methods:{
		consultarCategorias(){
			this.$loading(true);
			servicio.consultarCategorias(0)
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
		categoriaSeleccionada(idCategoria){
			this.$router
				.push({ path: `/Posts/0/${idCategoria}/0/false` })
				.catch(err => err)
			//console.log("categoriaSeleccionada " + idCategoria)
		}
	}
}
</script>
<style scoped>
.tamanoimagen {
	height: 300px !important;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>