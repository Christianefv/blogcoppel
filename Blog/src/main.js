import Vue from 'vue'
import './plugins/bootstrap-vue'
import App from './App.vue'
import router from './router'
import '@fortawesome/fontawesome-free/css/all.css'
import Msg from '@/plugins/msg'
import store from './store'
import Vuelidate from "vuelidate"
import VueFacebook from 'vue-facebook'
import globalesMixin from "@/mixins/globales"
import sideBarMixin from "@/mixins/sidebarmx"

Vue.config.productionTip = false
Vue.mixin(globalesMixin)
Vue.mixin(sideBarMixin);
Vue.use(Msg)
Vue.use(Vuelidate)
Vue.use(VueFacebook)

Vue.prototype.$loading = function(isLoading, msg="Espere un momento ..."){
  this.$store.commit('setLoading', isLoading)
  this.$store.commit('setMsgLoading', msg)
}

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')