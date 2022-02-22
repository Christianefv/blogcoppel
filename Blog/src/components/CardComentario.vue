<template>
    <div class="comment row">
        <div class="col-11 d-flex">
            <div class="avatar" v-if="comentario.avatar!=''">
                <img    :src="'data:image/jpeg;base64,' + comentario.avatar" 
                        alt="Responsive image">
            </div>
            <div class="text">
                <a class="username" @click="verUsuario">@{{ comentario.usuario }}</a> <span>{{ comentario.comentarios }}</span>
            </div>
        </div>
        <div class="col-1" v-if="comentario.idCatUsuarios == usuarioGlobal().idCatUsuario">
            <button class="btn text-danger" @click="eliminarComentario(comentario.idComentarios)">
                <i class="fas fa-trash"></i>
            </button>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'comentario',
        props: ['comentario'],
        methods: {
            verUsuario(){
                this.$emit("verUsuario", this.comentario.idCatUsuarios)
            },
            eliminarComentario(idComentarios){
                this.$emit("eliminarComentario", idComentarios)
            }
        }
    }
</script>

<style scoped>
/* Single-comment component */
.comment {    
    padding: 10px;
    margin-bottom: 10px;
    align-items: center;
    /* color: #333; */
    background-color: #EFB758;
    border-radius: 30px;
    box-shadow: 1px 1px 3px rgba(0, 0, 0, 0.2);
}

.comment .avatar {
    align-self: flex-start;
}

.comment .avatar > img {
    width: 40px;
    height: 40px;
    border-radius: 100%;
    align-self: start;
}

.comment .text {
    text-align: left;
    margin-left: 5px;
}

.comment .text span {
    margin-left: 5px;
}

.comment .text .username {
    font-weight: bold;
    color: #333;
}

</style>