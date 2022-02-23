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
                        <th>Editar</th>
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
                                    class="text-white btn bg-primary" 
                                    @click="editar(item)">
                                    <i class="fas fa-edit"></i>
                            </button>
                        </td>
                        <td class="text-center" 
                            style="width:30px!important">
                            <button style="border:none" 
                                    class="text-white bg-danger btn" 
                                    @click="eliminarPublicacion(item)">
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

        <!-- modal vista previa -->
        <b-modal id="modal-editar" size="xl" title="Editar" hide-footer>
        <div class="d-flex" >
            <pm-datos-publicacion   :categorias="categorias"
                                    :publicacion="publicacion"
                                    @guardarPublicacion="guardarPublicacion"
                                    @cerrarModalEditar="cerrarModalEditar">
            </pm-datos-publicacion>            
        </div>
    </b-modal> 
    </div>
</template>
<script>
import PmDatosPublicacion from "@/components/privada/EditarPublicacion"
import servicio from "@/services/servicio-publicacion"
import servicioCategorias from "@/services/servicio-categorias"
export default {
    data() {
        return {
           categorias:[],
           publicaciones:[],
           publicacion:{},
           publicacionRespaldo:{
               titulo:''
               ,descripcion:''
               ,idCatCategorias:0
               ,idCatPublicaciones:0
               ,idCatUsuarios:0}
        }
    },
    components: {
        PmDatosPublicacion
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
        async editar(item){
            await this.consultarCategorias()
            this.publicacion = item
            this.publicacionRespaldo.titulo = item.titulo
            this.publicacionRespaldo.descripcion = item.descripcion
            this.publicacionRespaldo.idCatCategorias = item.idCatCategorias
            this.publicacionRespaldo.idCatPublicaciones = item.idCatPublicaciones
            this.publicacionRespaldo.idCatUsuarios = item.idCatUsuarios
            this.$bvModal.show('modal-editar')
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
        async consultarCategorias(){
            this.$loading(true)
			await servicioCategorias.consultarCategorias(0)
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
        async guardarPublicacion(formData){
            this.$bvModal.hide('modal-editar')
            let r = await this.$msg.question('Se guardaran los cambios, ¿Desea continuar?') 
            if(r.value){
                this.$loading(true)
                servicio.modificarPublicaciones(formData)
				.then(r => {
					this.$loading(false)
					if(r.value){
						this.consultarPublicaciones()
					}
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
                console.log('tag', formData)
            }
            else{
                this.publicacion.titulo = this.publicacionRespaldo.titulo
                this.publicacion.descripcion = this.publicacionRespaldo.descripcion
                this.publicacion.idCatCategorias = this.publicacionRespaldo.idCatCategorias
                this.publicacion.idCatPublicaciones = this.publicacionRespaldo.idCatPublicaciones
                this.publicacion.idCatUsuarios = this.publicacionRespaldo.idCatUsuarios
            }
        },
        cerrarModalEditar(){
            this.$bvModal.hide('modal-editar')
            this.publicacion.titulo = this.publicacionRespaldo.titulo
            this.publicacion.descripcion = this.publicacionRespaldo.descripcion
            this.publicacion.idCatCategorias = this.publicacionRespaldo.idCatCategorias
            this.publicacion.idCatPublicaciones = this.publicacionRespaldo.idCatPublicaciones
            this.publicacion.idCatUsuarios = this.publicacionRespaldo.idCatUsuarios
        }
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
