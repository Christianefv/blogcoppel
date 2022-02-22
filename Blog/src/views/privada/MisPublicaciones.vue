<template>
    <div class="row">
        <div class="col-12 h4 text-center mt-4">
            Mis publicaciones
        </div>
        <div class="col-12" v-if="publicaciones.length > 0">
            <table  class="table table-sm table-sm-responsive table-bordered table-striped">
                <thead class="text-center bg-primary text-white">
                    <tr>
                        <th>Categoría</th>
                        <th>Titulo</th>
                        <th>Descripción</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody  v-for="(item, index) in publicaciones" 
                        :key="index">
                    <tr>                        
                        <td class="text-center">
                            {{item.categoria}}
                        </td>
                        <td class="text-center">
                            {{item.titulo}}
                        </td>
                        <td>
                            <span :title="item.descripcion" class="truncate-text cursor-pointer"> {{cortarTexto(item.descripcion, 25)}}</span>
                        </td>
                        <td class="text-center" 
                            style="width:30px!important">
                            <button style="border:none" 
                                    class="text-white bg-danger btn" 
                                    @click="eliminarPublicacion(item, index)">
                                    <i class="fas fa-trash"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div v-else class="bg-amarillo col text-center text-white ml-2 mr-2" style="height:30px !important;">
            <span> No hay publicaciones que mostrar</span>
        </div>
    </div>
</template>
<script>
import servicio from "@/services/servicio-publicacion"
export default {
    data() {
        return {
           publicaciones:[]
        }
    },
    computed:{

    },
    async mounted() {
        this.consultarPublicaciones()
    },
    methods: {
        consultarPublicaciones(){
			this.$loading(true)
			servicio.consultarPublicaciones(this.usuarioGlobal().idCatUsuario, 0, 0)
				.then(r => {
					//console.log(r.data)
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
        async eliminarPublicacion(item){
            let r = await this.$msg.question('¿Desea eliminar la publicación?') 
            if(r.value){
                this.$loading(true)
                servicio.eliminarrPublicaciones(item.idCatPublicaciones)
				.then(r => {
					//console.log(r.data)
					if(r.value){
						this.$loading(false)
						this.consultarPublicaciones()
					}
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
            }
        },
        editar(item){
            console.log(item)
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
    }
}
</script>
<style scoped>
.bg-amarillo{
    background-color:#EFB758 !important;
    font-weight: bold;
}

.truncate-text {
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
}
</style>
