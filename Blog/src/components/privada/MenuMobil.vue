<template>
	<div class="menu-mobile-container d-lg-none">
		<div class="row nav-movil overflow-auto no-gutters" id="menu-container">
			<!--<div class="col-12 overflow-auto px-0">-->
			<ul class="list-group list-group-horizontal px-2 w-100 d-flex align-items-center">
				<li @click="desplazar($event,'publicaciones-usuario',0)" 
					class="list-group-item" 
					title="Inicio">
					<router-link to="/usuarios/">
						<i 	:class="{ active: current == 'publicaciones-usuario'}" 
							class="fas fa-home"></i>
					</router-link>
				</li>
				<li class="list-group-item" 
					id="publicar" 
					title="Publicar">
					<router-link to="/publicar" 
						@click.native="desplazar($event,'publicar',1)" >
						<i 	:class="{ active: current == 'publicar'}" 
							class="fas fa-upload"></i>
					</router-link>
				</li>
				<li @click="desplazar($event,'publicaciones-usuarios',3)" 
					class="list-group-item" 
					id="publicaciones-usuarios" 
					title="Publicaciones de usuarios">
					<router-link to="/publicaciones-usuarios">
						<i 	:class="{ active: current == 'publicaciones-usuarios'}" 
							class="fas fa-comment"></i>
					</router-link>
				</li>
				<li v-if="usuarioGlobal().administrador" 
					@click="desplazar($event,'agregar-categoria',2)" 
					class="list-group-item" 
					id="agregar-categoria" 
					title="Agregar categoría">
					<router-link to="/agregar-categoria">
						<i 	:class="{ active: current == 'agregar-categoria'}" 
							class="fas fa-plus"></i>
					</router-link>
				</li>	
				<li v-if="usuarioGlobal().administrador" 
					@click="desplazar($event,'mis-categorias',4)" 
					class="list-group-item" 
					id="mis-categorias" 
					title="Mis categorías">
					<router-link to="/mis-categorias">
						<i 	:class="{ active: current == 'mis-categorias'}" 
							class="fas fa-list"></i>
					</router-link>
				</li>			
				<li @click="logOut" 
					class="list-group-item" 
					id="cerrar-sesion" 
					title="Salir">
						<button @click="logOut" class="px-4 btn" style="background:none; border;none">
							<i class="fa fa-sign-out-alt mr-1"></i>
						</button>
				</li>
			</ul>
		</div>
	</div>
</template>

<script>
	export default {
		name: "MenuMovil",
		data() {
			return {
				current: "publicaciones-usuario",
			}
		},
		created(){
		},
		mounted() {
			if (this.$route.path.includes("publicaciones-usuarios")) {
				this.current = 'publicaciones-usuarios'
			} else if (this.$route.path.includes("publicar")) {
				this.current = 'publicar'
			} else if (this.$route.path.includes("agregar-categoria")) {
				this.current = 'agregar-categoria'
			} else if (this.$route.path.includes("usuarios")) {
				this.current = 'usuarios'
			} else if (this.$route.path.includes("misCategorias")) {
				this.current = 'usuarios'
			}

		},
		directives: {
			
		},
		methods: {
			logOut() {
				window.localStorage.removeItem('refreshToken')
				window.localStorage.removeItem('tokenWebUsuarios')
				window.localStorage.removeItem('user')
				window.localStorage.removeItem('expiresAt')

				this.$router.push({ path: "/" })
				.catch(err => err)
			},
			desplazar(e, opcion, num) {
				
				this.currentSubMenu = opcion
					
				this.current = opcion
					

				var menuContainer = document.getElementById("menu-container")

				var resolucion = menuContainer.clientWidth
				var salto = 0
				if (e != undefined) {
					if (resolucion < 576) {
						if (e.clientX <= resolucion / 2) {
							salto = num > 3 ? 80 / num : 0
						} else {
							salto = 80 * num
						}
					} else {
						if (e.clientX <= resolucion / 2) {
							salto = 0
						} else {
							salto = 80 * num
						}
					}
				}

				menuContainer.scrollTo({
					left: salto,
					behavior: "smooth",
				})
			},
		},
		watch: {
		},
	}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
	.menu-mobile-container{
		position: relatave;
	}

	
	.routerLink{
		color: white;
		width: 100%;
	}

	.list-group-item {
		flex: 1 1 0px;
		text-align: center;
	}
	.nav-movil{
		background-color: #17395C !important;
		min-height: 85px;
		padding-top: 0.6rem;		
		overflow-y: hidden !important;
	}

	.nav-movil ul li i {
		font-size: 31px;
		color: white;
		background-color: #17395C;
		padding: 10px;
	}

	.nav-movil ul li {
		border: none;
		height: 62px;
		width: 62px;
		background-color: #17395C;
		padding: 0.45rem 0.55rem;
		/*margin-right: 1.3rem;*/
	}

	.nav-movil ul li i.active {
		color: #17395C !important;
		background-color:white !important;
        border-radius: 50%;
		transition: all 0.5s;
	}

	@media (max-width: 640px) {
		.nav-movil ul li i {
			padding: 3px !important;
		} 
	}

	@media (max-width: 360px) {
		.nav-movil {
			background-color: #17395C !important;
			min-height: 64px;
			padding: 0px;
		}

		.nav-movil ul li i {
			font-size: 23px;
			padding: 3px !important;
		}

		.nav-movil ul li,
		.nav-movil ul li.active {
			height: 48px;
			width: 48px;
			padding: 0.3rem 0.5rem;
			margin-right: 0.9rem;
			margin-top: 0.5rem;
		}
	}
</style>
