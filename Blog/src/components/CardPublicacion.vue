<template>
    <div>
        <div v-if="motoTarjeta=='true'" class="font-12 col-categoria mx-1">
            <div class="d-flex" >
                <div    :class="publicacion.imagen==null ? 'd-none':'d-flex align-items-center'">
                    <img    class="img-fluid card-categoria d-none d-md-none d-lg-block" 
                            v-bind:src="'data:image/jpeg;base64,' + publicacion.imagen" 
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
                <span> Autor: {{publicacion.autor}}</span>
            </div> 
        </div>
        <div v-else class="col cursor-pointer text-left bg-amarillo text-white m-1">
            <div class="row">
                <div class=" col-md-12 mt-2">{{publicacion.titulo}}</div>
                <div class="col-11 d-flex">
                    <div class="col-11">
                        <p class="truncate-text">
                            {{cortarTexto(publicacion.descripcion, 50)}}
                        </p>
                    </div>
                    <div class="col-1">
                        <i  class="fas fa-plus-circle fa-1x cursor-pointer" 
                            title="Ir a la publicación" 
                            @click="seleccionarPublicacion()"
                            role="button"></i>
                    </div>
                   
                </div>
            </div>
            <div class="col d-flex justify-content-end  text-rigth">
                <span> Autor: {{publicacion.autor}}</span>                
            </div> 
        </div>
    </div>
</template>
<script>
export default {
    props: ["publicacion","index", "motoTarjeta"],
    components: {
    },
    data() {
        return {
            indexLoc: this.index,
            comentarios: [

            ]
        }
    },		
    mounted() {		    
    },    
    methods: {
        seleccionarPublicacion(){
            this.$emit("seleccionarPublicacion", this.publicacion)
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

.card-categoria {
    width: 318px;
    height: 317px;
}
textarea {
  height:300px;
  resize: none !important;
  background-color: white !important;
  border: none !important;
}
.col-categoria {
    padding: 0px !important;
    position: relative;
    display: -ms-flexbox;
    display: -webkit-box;
    display: flex;
    -ms-flex-direction: column;
    -webkit-box-orient: vertical;
    -webkit-box-direction: normal;
    background-color: #fff;
    flex-direction: column;
    min-width: 0;
    word-wrap: break-word;
    background-clip: border-box;
    border: 1px solid rgba(0, 0, 0, 0.125);
    border-radius: 0.25rem;
}

.truncate-text {
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
}
</style>
