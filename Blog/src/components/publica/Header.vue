<template>
  <div >
    <div id="nav" class="d-flex justify-content-end bg-dark">
      <a class="m-2" @click="inicio">Inicio</a>
      <a class="m-2" @click="ultimasPublicaciones">Últimas publicaciones</a>
      <a class="m-2" @click="login">Inicia sesión o regístrate</a>
      <!-- <router-link to="/" class="m-2">Inicio </router-link>
      <router-link to="/Posts/0/0/0/false" class="m-2">Ultimas publicaciónes </router-link>
      <router-link to="/Login" class="m-2">Inicia sesión o regístrate </router-link> -->
    </div>
    <router-view/>
  </div>
</template>
<script>
export default {
  methods:{
    inicio(){
      this.$router.push({ path: "/" })
			.catch(err => err)
      this.$router.go()
    },
    ultimasPublicaciones(){
      this.$router.push({ path: "/Posts/0/0/0/false" })
			.catch(err => err)
      this.$router.go()
    },
    login(){
      this.$router.push({ path: "/login" })
			.catch(err => err)
      this.$router.go()
    }
  }
}
</script>
<style>
#nav {
  padding: 10px;
  background-color:#17395C !important;
}

#nav a {
  font-weight: bold;
  color: #EFB758;
}
#nav a:hover {
  font-weight: bold;
  color: white;
  cursor: pointer;
}
#nav a.link-exact-active {
  font-weight: bold;
  color: white !important;
}
</style>