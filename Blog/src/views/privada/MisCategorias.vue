<template>
    <div class="row">
        <div class="col-12 h4 text-center mt-4">
            Mis categorias
        </div>
       <div class="col-12" v-if="categorias.length > 0">
            <table  class="table table-sm table-sm-responsive table-bordered table-striped">
                <thead class="text-center bg-primary text-white">
                    <tr>
                        <th>Titulo</th>
                        <th>Descripción</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody  v-for="(item, index) in categorias" 
                        :key="index">
                    <tr>
                        <td class="text-center">
                            {{item.titulo}}
                        </td>
                        <td>
                            <span :title="item.descripcion" 
                            class="truncate-text cursor-pointer"> 
                            {{cortarTexto(item.descripcion, 25)}}</span>
                        </td>
                        <td class="text-center" 
                            style="width:30px!important">
                            <button style="border:none" 
                                    class="text-white bg-danger btn" 
                                    @click="eliminarCategoria(item)">
                                    <i class="fas fa-trash"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div v-else class="bg-amarillo col text-center text-white ml-2 mr-2" style="height:30px !important;">
            <span> No hay categorías que mostrar</span>
        </div>
    </div>
</template>
<script>
import servicioCategorias from "@/services/servicio-categorias"
export default {
    data() {
        return {
            categorias:[]
        }
    },
    components: {
	},
    mounted() {	
        this.consultarCategorias()
    },
    methods: {
        consultarCategorias(){
            this.$loading(true)
            servicioCategorias.consultarCategorias(0)
				.then(r => {
					if(r.value){
						this.categorias = r.data
					}
                    this.$loading(false)
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
        },
        cortarTexto(cadena, fin) {
            if (cadena != undefined) {
                if (cadena.length > fin) {
                    return cadena.substr(0, fin) + "..."
                } else {
                    return cadena.substr(0, cadena.length)
                }
            }
        },
        async eliminarCategoria(item){
            let r = await this.$msg.question('¿Desea eliminar la categoría?')
            if(r.value){
                this.$loading(true)
                servicioCategorias.eliminarCategorias(item.idCatCategorias)
				.then(r => {
					this.$loading(false)
					if(r.value){
                        this.$msg.success(r.message)
						this.consultarCategorias()
					}
                    else{
                         this.$msg.info('Ocurrió un error al eliminar la categoría.');
                    }
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
            }
        },
    }
}
</script>
<style scoped>
    
</style>