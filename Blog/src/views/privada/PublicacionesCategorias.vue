<template>
    <div>
        <div class="col-12 h4 text-center mt-4">
            Todas las publicaciones
        </div>
		<div class="col-12">
            <div class="form-group row">
                <label  class="col-3 col-form-label text-sm-right pr-0">
                    <b>Ver por categoría:</b></label>            
                <div class="col-8">
                    <select class="form-control form-control-sm input-select"
                        @change="consultarPublicaciones(0)"
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
        <div v-if="modoTarjeta=='true'">
            <card-comentarios
            :comentarios="comentarios"
            @comentar="comentar"
            @verUsuario="verUsuario"></card-comentarios>
        </div>
        <b-modal id="modal-usuario" size="xl" title="Detalles del usuario" hide-footer>
            <card-usuario
                :usuario="usuarioDetalle">
            </card-usuario>
            
            <b-button class="mt-3" block @click="$bvModal.hide('modal-usuario')">Cerrar</b-button>
        </b-modal> 
    </div>
</template>
<script>
import servicio from "@/services/servicio-publicacion"
import servicioInicio from "@/services/servicio-inicio"
import servicioUsuarios from "@/services/servicio-usuario"
import CardPublicacion from "@/components/CardPublicacion"
import CardComentarios from "@/components/CardComentarios"
import CardUsuario from "@/components/privada/CardUsuario"

export default({
    data() {
        return {
            categorias:[],
            idCategoriaSeleccionada:0,
            publicaciones:[],
            modoTarjeta:false,
            idUsuario:0,
            idPublicacion:0,
            comentarios:[],
            comentario:{
                idCatUsuarios:0,
                idCatPublicaciones:0,
                comentarios:''
            },
            usuarioDetalle:{}
        }
    },
    components: {
		CardPublicacion,
        CardComentarios,
        CardUsuario
	},
    computed:{
    },
    mounted() {
        this.consultarCategorias()
        this.consultarPublicaciones(0)
        //this.consultarPublicaciones(0,this.idCategoriaSeleccionada, this.idPublicacion)
    },
    methods: {
        consultarCategorias(){
			this.$loading(true);
			servicioInicio.consultarCategorias(0)
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
        async consultarPublicaciones(idPublicacion){
            this.modoTarjeta = 'false'
			this.$loading(true)
			await servicio.consultarPublicaciones( 0, this.idCategoriaSeleccionada, idPublicacion)
				.then(r => {
					if(r.value){
						this.$loading(false)
						this.publicaciones = r.data
                        console.log('console.logidCatPublicaciones', this.publicaciones[0].idCatPublicaciones)
					}
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
		},
        async seleccionarPublicacion(publicacion){
            await this.consultarPublicaciones(publicacion.idCatPublicaciones)
            this.modoTarjeta = 'true'
            this.consultarComentarios(publicacion.idCatPublicaciones)
        },
        consultarComentarios(idCatPublicaciones){
            this.$loading(true)
			servicio.consultarComentarios( idCatPublicaciones)
				.then(r => {
					if(r.value){
						this.$loading(false)
						this.comentarios = r.data
					}
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
        },
        comentar(comentariotext){
            this.$loading(true)

            this.comentario.idCatUsuarios = this.usuarioGlobal().idCatUsuario
            this.comentario.idCatPublicaciones = this.publicaciones[0].idCatPublicaciones
            this.comentario.comentarios = comentariotext
			
            servicio.guardarComentarios( this.comentario)
				.then(r => {
					if(r.value){
						this.$loading(false)
						this.consultarComentarios(this.comentario.idCatPublicaciones)
					}
				})
				.catch(e => {
					console.log(e)
					this.$loading(false)
				})
        },
        verUsuario(idCatUsuarios){
            this.usuarioDetalle = {}
            servicioUsuarios.consultarDatosUsuario(idCatUsuarios)
                .then(r => {
                    if(r.value){
                        this.usuarioDetalle =  r.data[0]
                        this.$bvModal.show('modal-usuario')
                    }
                })
                .catch(err => {
                    console.log(err)
                })
        }
    }
})
</script>
<style scoped>
</style>