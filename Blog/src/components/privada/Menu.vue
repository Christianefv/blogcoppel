<template>
	<div class="container-fluid">
		<div class="row" id="row-contenido">			
			<div class="col-2 d-none d-lg-flex side-bar bg-white pl-0">
				<div class="blue-bar"></div>
				<ul class="nav flex-column flex-nowrap overflow-hidden blue-bar-over">
					<li class="nav-item text-left my-3">
						<router-link class="text-truncate text-decoration-none" @click.native="togleMenu(0)" to="/usuarios/">
						<div v-if="usuarioGlobal().avatar != ''" class="avatar">
							<img class="img-fluid" :src="'data:image/jpeg;base64,' + usuarioGlobal().avatar" alt="image responsive">
						</div>
						<span class="d-none d-sm-inline welcome-text">{{usuarioGlobal().usuario}}</span>
							<!-- <span class="d-none d-sm-inline welcome-text">Bienvenido</span> -->
						</router-link>
					</li>
					<li class="nav-item text-left">
						<router-link
							:class="{ active: current == 0 }"
							@click.native="togleMenu(0)"
							class="nav-link text-truncate"
							to="/usuarios"
						>
							<i
								:class="[current == 0 ? 'text-white' : '','blue-bar-icon']"
								class="fas fa-home"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Inicio</span>
						</router-link>
					</li>
					<li class="nav-item text-left">
						<router-link
							:class="{ active: current == 1 }"
							@click.native="togleMenu(1)"
							class="nav-link text-truncate"
							to="/publicar"
						>
							<i	:class="[current == 1 ? 'text-white' : '','blue-bar-icon']"
								class="fas fa-upload"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Publicar</span>
						</router-link>
					</li>
					<li class="nav-item text-left" v-if="usuarioGlobal().administrador">
						<router-link
							:class="{ active: current == 2 }"
							@click.native="togleMenu(2)"
							class="nav-link text-truncate"
							to="/agregar-categoria"
						>
							<i :class="[ current == 2 ? 'text-white' : '', 'blue-bar-icon']"
								class="fas fa-plus"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Agregar categoria</span>
						</router-link>
					</li>
					<li class="nav-item text-left">
						<router-link
							:class="{ active: current == 3 }"
							@click.native="togleMenu(3)"
							class="nav-link text-truncate"
							to="/publicaciones-usuarios"
						>
							<i :class="[ current == 3 ? 'text-white' : '', 'blue-bar-icon']"
								class="fas fa-comment"
							></i>
							<span class="nav-link-text d-none d-sm-inline">Todas las publicaciones</span>
						</router-link>
					</li>
					<li class="nav-item text-left">
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
					</li>
				</ul>		
			</div>
			<!-- fin sección side-bar -->

			<!-- Inicia sección contenido -->
			<div class="col pt-2 bg-gray text-left">
				<slot name="contenido"></slot>
			</div>
			<!-- Fin sección contenido -->
		</div>
	</div>
</template>

<script>
	export default {
		name: "SideBar",
		props: [],
		data() {
			return {
				current: 0,			
			}
		},
		mounted() {
			if (this.$route.path.includes("publicaciones-usuarios")) {
				this.current = 3
			} else if (this.$route.path.includes("publicar")) {
				this.current = 1
			} else if (this.$route.path.includes("agregar-categoria")) {
				this.current = 2
			} else if (this.$route.path.includes("usuarios")) {
				this.current = 0
			} 
		},
		methods: {
			togleMenu: function(opcion) {
				this.current = opcion
				if(opcion == 3){
					this.$router.go()	
				}
			},
			logOut() {
				window.localStorage.removeItem("tokenWebUsuarios")

				this.$router.push({ path: "/" })
					.catch(err => err)
			}
		},
		watch: {
		}
	}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
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
	.bg-gray {
		background-color: white;
	}

	.nav-link {
		padding: 0.4rem 0.5rem;
		margin-top: 0.1rem;
		color: black;
		font-size: 16px;
		font-weight: bold;
	}

	.nav-item .active {
		background: #17a2b8;
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
		color: #0a2157;
		font-weight: bold;
		font-size: 20px;
	}

	.blue-bar-icon {
		color: #0a2157;
		font-size: 23px;
	}
</style>
