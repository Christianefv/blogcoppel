<template>
    <div>        
        <!-- <pm-header></pm-header> -->
        <pm-menu-movil
		></pm-menu-movil>
        <sidebar-menu>
            <template slot="contenido">
                <router-view
                    :data="menu"
                ></router-view>
            </template>
        </sidebar-menu>
    </div>
</template>
<script>
// import PmHeader from "@/components/privada/Header"
import SidebarMenu from "@/components/privada/Menu"
import PmMenuMovil from "@/components/privada/MenuMobil"
export default {
    data() {
        return{
            menu: [
                {
                    href: '/publicar',
                    title: 'Publicar',
                    icon: 'fa fa-user'
                },
                {
                    href: '/agregar-categoria',
                    title: 'Agregar categoria',
                    icon: 'fa fa-chart-area'
                },
                {
                    href: '/',
                    title: 'Dashboard',
                    icon: 'fa fa-user'
                },
                {
                    href: '#',
                    title: 'Charts',
                    icon: 'fa fa-chart-area'
                },
                {
                    href: '/',
                    title: 'Dashboard',
                    icon: 'fa fa-user'
                },
                {
                    href: '#',
                    title: 'Charts',
                    icon: 'fa fa-chart-area'
                },                
            ]
        }
    },
    components: {
        SidebarMenu,
        PmMenuMovil,
        // PmHeader
    }
}
</script>
<style scoped>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 10px;
  background-color:cadetblue;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: white;
}
</style>