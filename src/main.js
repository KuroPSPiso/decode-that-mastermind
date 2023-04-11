import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import store from './store'
import vuetify from './plugins/vuetify'
import vueWorker from 'vue-worker'

Vue.config.productionTip = false
Vue.use(vueWorker)

new Vue({
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
