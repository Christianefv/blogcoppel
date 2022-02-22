<template>
    <div class="comments">
        <div class="h5 text-center">
            Comentarios
        </div>
        <div :class="comments_wrapper_classes">
            <card-comentario                
                v-for="(comentario, index) in toBeShown"
                :comentario="comentario"
                :key="index"
                @verUsuario="verUsuario"
                @eliminarComentario="eliminarComentario"
            ></card-comentario>
        </div>
         <div class="d-flex justify-content-end">
            <button class="btn font-11" @click="verMenos" :disabled="currentPage==1">Ver menos...</button>
            <button class="btn font-11" @click="verMas" :disabled="currentPage == totalPages">Ver más...</button>
        </div>
        <hr>
        <div class="reply">
            <div class="avatar" v-if="usuarioGlobal().avatar!=''">
                <img    class="img-fluid"
                        :src="'data:image/jpeg;base64,' + usuarioGlobal().avatar" 
                        alt="Responsive image">
            </div>
            <input 
                type="text" 
                v-model.trim="reply" 
                class="reply--text" 
                placeholder="Comentario..."
                required
                @keyup.enter="comentar"/>
            <button class="reply--button" 
                    @click.prevent="comentar">
                    <i class="fa fa-paper-plane"></i> 
                    Enviar
            </button>
        </div>
    </div>
</template>

<script>
import CardComentario from "@/components/CardComentario"
    
    export default {
        name: 'comments',
        props: ['comentarios', 'usuario', 'comments_wrapper_classes'],
        components: {
            CardComentario
        },
        computed:{
            toBeShown() {
                return this.comentarios.slice(0, this.currentPage * 4);
            },
            totalPages() {
                return Math.ceil( this.comentarios.length / 4);
            }
        },
        data() {
            return {
                reply: '',
                currentPage: 1,
            }
        },
        methods: {
            verMas(){
                if(this.currentPage <  this.totalPages) this.currentPage++;
            },
            verMenos(){
                this.currentPage = this.currentPage - 1 || 1;
            },
            comentar() {
                if(this.reply != '') {
                    this.$emit('comentar', this.reply);
                    this.reply = '';
                }
            },
            verUsuario(idCatUsuarios){
                this.$emit("verUsuario", idCatUsuarios)
            },
            eliminarComentario(idComentarios){
                this.$emit("eliminarComentario", idComentarios)
            }
        },
        
    }
</script>

<style scoped>
.font-11{
    font-size: 11px;
}
.comments {
    margin-top: 20px;
    padding: 20px;
    padding-top: 0;
}

.comments-wrapper {
    max-height: 250px;
    overflow-y: auto;
    padding-right: 10px;
}
/* Reply component */
.reply {
    display: flex;
    position: relative;
    align-items: center;
    background-color: #eec57f;
    border-radius: 30px;
    padding: 5px 10px;
    overflow: hidden;
}

.reply .avatar {
    position: absolute;
}

.reply .avatar > img {
    width: 40px;
    height: 40px;
    border-radius: 100%;
}

.reply .reply--text {
    min-height: 40px;
    padding: 10px 10px 10px 55px;
    margin-right: 10px;
    border: 0;
    color: #333;
    width: 100%;
    outline: 0;
    background-color: transparent;
    box-shadow: none;
}

.reply input.reply--text:valid {
    margin-right: 71px;
}

.reply input.reply--text:valid + .reply--button {
    right: 10px;
}

.reply .reply--button {
    position: absolute;
    right: -100px;
    border: 1px solid #17395C;
    background-color: transparent;
    color: #17395C;
    display: inline-block;
    font-weight: 400;
    text-align: center;
    white-space: nowrap;
    vertical-align: middle;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
    padding: 0.375rem 0.75rem;
    font-size: 15px;
    line-height: 1.5;
    border-radius: 30px;
    transition: color 0.25s ease-in-out, background-color 0.25s ease-in-out, border-color 0.25s ease-in-out, box-shadow 0.25s ease-in-out, right 0.25s ease-in-out;
    outline: 0;
}

.reply .reply--button:hover {
    color: #fff;
    background-color: #2a629c;
}

.reply .reply--button:focus,
.reply .reply--button:active {
    box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
}

hr {
    margin-top: 10px;
    margin-bottom: 10px;
}
</style>
