<template>
    <div class="row">
        <pm-header class="col-12"></pm-header>
        <div class="container h-100 mt-4">
            <div class="row h-100 align-items-center justify-content-center">
                <div class="col-12 col-sm-7 col-md-5 col-lg-4">
                    <form class="bg-smoked rounded p-3" role="none">
                        <br>
                        <div class="text-center my-2">
                            <i class="fas fa-user-tie user"></i>
                        </div>
                        <h3 class="text-white text-center">Inicio de sesión</h3>
                        <p class="text-white">Ingresa usuario y contraseña para iniciar</p>
                        <div class="form-group">
                            <label for="txtUsuario" class="sr-only">Usuario:</label>
                            <input  type="text" 
                                    id="txtUsuario" 
                                    name="txtUsuario" 
                                    class="form-control" 
                                    placeholder="Usuario" required 
                                     v-model="credenciales.usuario">
                        </div>
                        <div class="form-group">
                            <label  for="txtPassword" 
                                    class="sr-only">Contraseña:</label>
                            <input  type="password" 
                                    id="txtPassword" 
                                    name="txtPassword" 
                                    class="form-control" 
                                    placeholder="Contraseña" required 
                                    v-model="credenciales.password">
                        </div>
                        <br>
                        <button class="btn btn-lg btn-outline-warning btn-block" 
                                name="btnEntrar" 
                                @click.prevent="login">Entrar</button>
                        <span style="color:#F00" align="center">
                            <strong></strong>
                        </span>
                        <div class="mt-2">
                            <router-link to="/registro" class="btn btn-lg btn-block btn-outline-light">Registrate </router-link>
                            <router-view/>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        
    </div>
</template>
<script>
import servicio from '@/services/servicio-login'
import PmHeader from "@/components/publica/Header"
export default {
    data(){
        return {
            credenciales:{
                usuario: '',
                password: ''
            },
            tokenWebUsuarios: {
                token:null,
                expiresAt:null,
                user:null,
                refreshToken:null,
            }
        }
    },
    components: {
		PmHeader
	},
    methods:{
        async login(){
            var r = await servicio.login(this.credenciales)
            if (r.value) {
                console.log('servicio.login(this.credenciales)',)
                this.tokenWebUsuarios.accessToken = r.token.accessToken
                this.tokenWebUsuarios.token = r.token
                this.tokenWebUsuarios.expiresAt = r.token.expiresAt
                this.tokenWebUsuarios.refreshToken = r.token.refreshToken
                this.tokenWebUsuarios.user = JSON.stringify(r.data)
                var decode = decodeURIComponent(encodeURI(this.tokenWebUsuarios.user))
                this.tokenWebUsuarios.user = decode
                window.localStorage.tokenWebUsuarios = JSON.stringify(this.tokenWebUsuarios)
                window.localStorage.user = r.data.usuario
                window.localStorage.expiresAt = r.token.expiresAt
                window.localStorage.refreshToken = r.token.refreshToken
                window.localStorage.accessToken = r.token.accessToken
                
                this.$router.push({ path: "/usuarios" })
                    .catch(err => err)
            } else {
                this.$msg.error(r.message)
            }
        }
    }
}
</script>

<style scoped>
.user{
    color: #fff;
    font-size: 5em;
}
.login-wrapper {
    background-position: center center !important;
    background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: cover;
}

.bg-smoked {
    background-color: #17395C !important;
    border: 1px solid rgba(245, 245, 245, .6) !important;
}

.form-control:focus {
    color: #495057;
    background-color: #fff;
    border-color: #17395C;
    outline: 0;
    -webkit-box-shadow: 0 0 0 0.2rem rgba(0, 0, 0, 0.1) inset;
    -moz-box-shadow: 0 0 0 0.2rem rgba(0, 0, 0, 0.1) inset;
    box-shadow: 0 0 0 0.2rem rgba(0, 0, 0, 0.1) inset;
}

.form-control::-webkit-input-placeholder {
    color: #fff;
    opacity: 6;
}

.form-control::-moz-placeholder {
    color: #fff;
    opacity: 1;
}

.form-control:-ms-input-placeholder {
    color: #fff;
    opacity: 1;
}

.form-control::-ms-input-placeholder {
    color: #fff;
    opacity: 1;
}

.form-control::placeholder {
    color: #fff;
    opacity: 1;
}

</style>