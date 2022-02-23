<template>
	<div class="container-fluid">
		<div class="row" id="row-contenido">
			<div class="col-2 d-none d-lg-flex side-bar bg-azul" style="min-height: 100vh">
				<ul class="nav flex-column flex-nowrap overflow-hidden blue-bar-over">
					<li class="nav-item text-left my-3" style="">
						<router-link class="text-truncate text-decoration-none" @click.native="togleMenu(0)" to="/usuarios/">
						<span class="d-none d-sm-inline welcome-text">{{usuarioGlobal().usuario}}</span>
						</router-link>
					</li>
					<li class="nav-item text-left">
						<router-link
							:class="{ active: current == 1 }"
							@click.native="togleMenu(1)"
							class="nav-link text-truncate"
							to="/usuarios"
						>
							<i
								:class="[current == 1 ? 'text-white' : '','blue-bar-icon']"
								class="fas fa-home"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Inicio</span>
						</router-link>
					</li>
					<li class="nav-item text-left">
						<router-link
							:class="{ active: current == 2 }"
							@click.native="togleMenu(2)"
							class="nav-link text-truncate"
							to="/publicar"
						>
							<i	:class="[current == 2 ? 'text-white' : '','blue-bar-icon']"
								class="fas fa-upload"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Publicar</span>
						</router-link>
					</li>
					<li class="nav-item text-left" >
						<router-link
							:class="{ active: current == 4 }"
							@click.native="togleMenu(4)"
							class="nav-link text-truncate"
							to="/publicaciones-usuarios"
						>
							<i :class="[ current == 4 ? 'text-white' : '', 'blue-bar-icon']"
								class="fas fa-comment"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Todas las publicaciones</span>
						</router-link>
					</li>
					<li class="nav-item text-left" v-if="usuarioGlobal().administrador">
						<router-link
							:class="{ active: current == 3 }"
							@click.native="togleMenu(3)"
							class="nav-link text-truncate"
							to="/agregar-categoria"
						>
							<i :class="[ current == 3 ? 'text-white' : '', 'blue-bar-icon']"
								class="fas fa-plus"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Agregar categoria</span>
						</router-link>
					</li>
					<li class="nav-item text-left" v-if="usuarioGlobal().administrador">
						<router-link
							:class="{ active: current == 5 }"
							@click.native="togleMenu(5)"
							class="nav-link text-truncate"
							to="/mis-categorias"
						>
							<i :class="[ current == 5 ? 'text-white' : '', 'blue-bar-icon']"
								class="fas fa-list"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Mis categorías</span>
						</router-link>
					</li>
					<!-- <li class="nav-item text-left">
						<router-link
							:class="{ active: current == 5 }"
							@click.native="logOut"
							class="nav-link text-truncate"
							to="/"
						>
							<i :class="[ current == 5 ? 'text-white' : '', 'blue-bar-icon']"
								class="fas fa-arrow-left"></i>
							<span class="nav-link-text d-none d-sm-inline">Cerrar sesión</span>
						</router-link>
					</li> -->
				</ul>		
			</div>
			<!-- fin sección side-bar -->

			<!-- Inicia sección contenido -->
			<div class="col text-left">
				<b-navbar class="row d-none d-lg-flex bg-azul">
					<b-navbar-brand class="d-flex align-items-center">
						<div v-if="usuarioGlobal().avatar != ''" class="avatar">
							<img class="img-fluid" :src="'data:image/jpeg;base64,' + usuarioGlobal().avatar" alt="image responsive">
						</div>
					</b-navbar-brand>					
					<b-navbar-nav class="ml-auto font-weight-bold text-warning">
						<b-nav-item ><span class="text-warning" style="color:#EFB758 !important;"> {{usuarioGlobal().nombre}}</span></b-nav-item>
						<button @click="logOut" class="px-4 btn btn-outline-warning">
							<i class="fa fa-sign-out-alt mr-1"></i> Salir
						</button>
					</b-navbar-nav>
				</b-navbar>
				<slot name="contenido"></slot>
			</div>
			<!-- Fin sección contenido -->
		</div>
	</div>
</template>

<script>
	export default {
		name: "SideBar",
		props: ["opcion"],
		data() {
			return {
				current: 0,			
			}
		},
		mounted() {
			if (this.$route.path.includes("publicaciones-usuarios")) {
				this.current = 4
			} else if (this.$route.path.includes("publicar")) {
				this.current = 2
			} else if (this.$route.path.includes("agregar-categoria")) {
				this.current = 3
			} else if (this.$route.path.includes("usuarios")) {
				this.current = 1
			} else if (this.$route.path.includes("mis-categorias")) {
				this.current = 5
			} 
		},
		methods: {
			togleMenu: function(opcion) {
				console.log('togleMenu: function(opcion)', opcion)
				this.current = opcion
				if(opcion == 3){
					this.$router.go()	
				}
			},
			logOut() {
				window.localStorage.removeItem('refreshToken')
				window.localStorage.removeItem('tokenWebUsuarios')
				window.localStorage.removeItem('user')
				window.localStorage.removeItem('expiresAt')

				this.$router.push({ path: "/" })
					.catch(err => err)
			}
		},
		watch: {
			opcion(newValue){
				console.log('newvalue', newValue)
				if(newValue > 0){
					this.togleMenu(newValue)
				}
				
			}
		}
	}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.bg-azul{
    background-color:#17395C !important;
    font-weight: bold;
}
	.avatar {
		position: absolute;
	}

	.avatar > img {
		width: 40px;
		height: 40px;
		border-radius: 20px;
	}

	.side-bar {
		min-width: 250px !important;
	}

	.nav-link {
		padding: 0.4rem 0.5rem;
		margin-top: 0.1rem;
		color: #EFB758;
		font-size: 16px;
		font-weight: bold;
	}

	.nav-item .active {
		background: #EFB758;
		color: white !important;
	}	
	.nav-link-text {
		margin-left: 1.5rem;
		font-size: 18px;
		font-weight: bold;
	}

	.blue-bar {
		position: relative;
		width: 48px;
	}

	.blue-bar-over {
		position: absolute;
		top: 0;
		left: 0;
		right: 0;
	}

	.welcome-text {
		margin-left: 4rem;
		color: #EFB758;
		font-weight: bold;
		font-size: 20px;
	}

	.blue-bar-icon {
		color: #EFB758;
		font-size: 23px;
	}
</style>
