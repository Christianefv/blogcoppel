<template>
    <div>
        <div class="font-12 col-categoria font-blue-1 mx-1">
            <div v-if="indexLoc%2==0"
                class="d-flex" >
                <div    :class="categoria.imagen=='' ? 'd-none':'d-flex align-items-center'">
                    <img    class="img-fluid card-categoria d-none d-md-none d-lg-block" 
                            v-bind:src="'data:image/jpeg;base64,' + categoria.imagen" 
                            alt="Responsive image"/>
                </div>
                <div class="ml-auto w-100 p-3">
                    <div class="font-weight-bold font-poppins">
                        <a  @click="seleccionarCategoria(categoria.idCatCategorias)" class="cursor-pointer" role="button">
                            {{categoria.titulo}}</a>
                    </div>
                    <div class="font-weight-bolder ml-2">
                        <textarea v-model="categoria.descripcion" class="form-control" style="height:300px" disabled></textarea></div>
                </div>
            </div>
            <div v-if="indexLoc%2==1"
                class="d-flex" >                
                <div class="ml-auto w-100 p-3">
                    <div class="font-weight-bold font-poppins">
                        <a  @click="seleccionarCategoria(categoria.idCatCategorias)" class="cursor-pointer" role="button">
                            {{categoria.titulo}}</a>
                    </div>
                    <div class="font-weight-bolder ml-2">
                        <textarea v-model="categoria.descripcion" class="form-control" style="height:300px" disabled></textarea></div>
                </div>
                <div    :class="categoria.imagen=='' ? 'd-none':'d-flex align-items-center'">
                    <img    class="img-fluid card-categoria d-none d-md-none d-lg-block" 
                            v-bind:src="'data:image/jpeg;base64,' + categoria.imagen" 
                            alt="Responsive image"/>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ["categoria","index", "motoTarjeta"],
    data() {
        return {
            indexLoc: this.index
        }
    },		
    mounted() {		    
    },
    methods: {
        seleccionarCategoria(idCategoria){
            this.$emit("categoriaSeleccionada", idCategoria)
        },
    }
}
</script>
<style>
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
