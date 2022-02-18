import Vue from 'vue'
import VueRouter from 'vue-router'
//import Home from '../views/Home.vue'

import Inicio from '@/views/publica/Inicio'
import Posts from '@/views/publica/Posts'
import Login from '@/views/publica/Login'
import Registro from '@/views/publica/Registro'
import Layout from '@/views/privada/Layout'
import Publicar from '@/views/privada/Publicar'
import MisPublicaciones from '@/views/privada/MisPublicaciones'
import PublicacionesCategorias from '@/views/privada/PublicacionesCategorias'
import Categorias from '@/views/privada/Categorias'
Vue.use(VueRouter)

const routes = [  
{
  path: '/',
  component: Inicio,
  meta: { isPublic: true }
},
{
  path: '/Posts/:idCatUsuarios/:idCatCategorias/:idCatPublicaciones/:modoTarjeta',
  component: Posts,
  meta: { isPublic: true }
},
{
  path: '/login',
  component: Login,
  meta: { isPublic: true }
},
{
  path: '/registro',
  component: Registro,
  meta: { isPublic: true }
},
{  
  path: '/usuarios',
  component: Layout,
  children: [
    { name: 'Publicar', path: '/publicar/', component: Publicar},
    { name: 'Categorias', path: '/agregar-categoria', component: Categorias },
    { name: 'MisPublicaciones', path: '', component: MisPublicaciones},
    { name: 'PublicacionesCategorias', path: '/publicaciones-usuarios', component: PublicacionesCategorias}
  ]
},
]


const router = new VueRouter({
  base: process.env.BASE_URL,
  routes
})

const isAutenticated = () => {
  
	if(window.localStorage.user){
		var date = new Date(window.localStorage.expiresAt);

		if(date < Date.now()){
			window.localStorage.removeItem('refreshToken')
      window.localStorage.removeItem('tokenWebUsuarios')
      window.localStorage.removeItem('user')
      window.localStorage.removeItem('expiresAt')

			this.$msg.fire({
			text: 'La sesión ha caducado, por favor vuelva a autenticarse para continuar',
			icon: 'warning'
			})
		}
	}

	return window.localStorage.tokenWebUsuarios
}

	router.beforeEach((to, from, next) => {
    
		if(!to.meta.isPublic && !isAutenticated()){
			next({path: '/'})
		}

		if(to.name == 'login' && isAutenticated()){
			return next({ path: '/usuarios/' })
		}

		return next()
	})

export default router
