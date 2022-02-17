<template>
    <div class="container">
        <pm-header class="col-12"></pm-header>

        <div class="col-12 offset-0 offset-lg-3 col-lg-6 mt-4">
            <div class="row justify-content-center">
                <span class="h3">Registrarte</span>
            </div>
            <div class="row p-0 mt-4">            
                <div class="form-group col-12 col-lg-6">
                    <label class="fuente-label-modal">Nombre del contacto</label>
                    <input
                        :class="{ 'is-invalid': $v.usuario.nombre.$error }"
                        class="form-control"
                        placeholder="Nombre"
                        ref="nombre"
                        type="text"
                        v-model="$v.usuario.nombre.$model"                        
                    />
                </div>        
                <div class="form-group col-12 col-lg-6">
                    <label class="fuente-label-modal">Correo electr&oacute;nico</label>
                    <input
                        :class="{ 'is-invalid': $v.usuario.correo.$error }"
                        class="form-control"
                        placeholder="micorreo@ejemplo.com"
                        ref="correo"
                        type="text"
                        v-model="$v.usuario.correo.$model"
                    />
                </div>
                <div class="form-group col-12 col-lg-6">
                    <label class="fuente-label-modal">Usuario</label>
                    <input
                        :class="{ 'is-invalid': $v.usuario.usuario.$error }"
                        class="form-control"
                        placeholder="Usuario"
                        ref="usuario"
                        type="text"
                        v-model="$v.usuario.usuario.$model"
                    />
                </div>
                <div class="col-12 d-flex text-left">
                    <p class="text-center">Contraseña</p>
                </div>
                <div class="form-group col-12 col-lg-6">
                    <input
                        :class="{ 'is-invalid': !$v.usuario.password.minLength }"
                        class="form-control my-2"
                        maxlength="50"
                        placeholder="Contraseña"
                        ref="pass"
                        type="password"
                        v-model="$v.usuario.password.$model"
                    />
                </div>
                <div class="form-group col-12 col-lg-6">
                    <input
                        :class="{ 'is-invalid': !$v.usuario.repeatPassword.sameAsPassword }"
                        class="form-control my-1"
                        maxlength="50"
                        placeholder="Confirmar contraseña"
                        type="password"
                        v-model="$v.usuario.repeatPassword.$model"
                    />
                </div>
                <div class="form-group row col-12">
                    <label class="col-3 col-form-label text-sm-right pr-0">Foto de perfíl:</label>
                    <div class="col-12 col-lg-1">
                        <div class="input-group-append">
                            <div class="input-group-text form-control-sm inputFilesAppendPrepend">
                                <i href="#" @click="loadFiles" class="far fa-image"></i>
                            </div>
                        </div>
                        <input type="file" id="archivo" ref="archivo" v-on:change="handleFilesUpload" accept=".jpeg, .jpg" style="display: none;" />
                    </div>
                    <div div class="input-group-append col-6 col-lg-7">
                        <span v-if="file.length==0" class="form-control-sm">* Solo archivos JPG</span>
                    </div>
                </div>
            </div>
            <div class="row justify-content-end">
                <button class="btn btn-outline-primary"
                        @click="altaUsuario">Guardar</button>
            </div>
        </div>        
    </div>
</template>

<script>
import { required, minLength, email, sameAs } from "vuelidate/lib/validators"	
import PmHeader from "@/components/publica/Header"
import servicio from "@/services/servicio-alta-usuario"
export default {
    props:["visible"],
    data(){
        return {
            usuario: {
                nombre: "",
                usuario: "",
                correo: "",
                password: "",
                repeatPassword: ""
            },
            file:[],
        }
    },
    components:{
        PmHeader
    },
    mounted(){
    },
    methods:{
        loadFiles() {
            console.log('click')
            this.$refs.archivo.click();
        },
        handleFilesUpload() {
            this.file = this.$refs.archivo.files[0]
        },
        altaUsuario(){           
            this.$v.$touch()
			if (this.$v.$invalid) return

            let formData = new FormData()
            formData.append('usuario', this.usuario.usuario)
            formData.append('nombre', this.usuario.nombre)
            formData.append('password', this.usuario.password)
            formData.append('correo', this.usuario.correo)
            formData.append('file', this.file);

            servicio.altaUsuario(formData)
                .then(r=>{
                    if(r.value){
                        this.$msg.success(r.message)
                        this.$router.push({ path: "/" })
                            .catch(err => err)
                    }
                    else{
                        this.$msg.info(r.message)
                    }
                })
                .catch(err => {
                    console.log(err)
                })
        }
    },
    validations: {
        usuario: {
            usuario: {
                required,
                minLength: minLength(2)
            },
            nombre: {
                required,
                minLength: minLength(2)
            },
            correo: {
                email,
                required,
                minLength: minLength(2)
            },
            password: {
                required,
                minLength: minLength(6)
            },
            repeatPassword: {
                sameAsPassword: sameAs('password')
            }
        }
    }
}
</script>

<style scoped>

</style>