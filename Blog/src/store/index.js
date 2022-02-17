import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    loading: false,
    msgLoading: 'Espere un momento ...'
  },
  getters:{
  },
  mutations: {
    setLoading(state, isLoading){
      state.loading = isLoading
    },
    setMsgLoading(state, msg){
      state.msgLoading = msg
    }
  },
  actions: {
  },
  modules: {
  }
})
