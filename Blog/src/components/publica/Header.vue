<template>
  <div >
    <div id="nav" class="d-flex justify-content-end bg-dark">
      <router-link to="/" class="m-2">Inicio </router-link>
      <router-link to="/Posts/0/0/0/false" class="m-2">Ultimas publicaciónes </router-link>
      <router-link to="/Login" class="m-2">Inicia sesión o regístrate </router-link>
    </div>
    <router-view/>
  </div>
</template>

<style>
#nav {
  padding: 10px;
  background-color:#17a2b8 !important;
}

#nav a.router-link-exact-active {
  color: white !important;
}
</style>