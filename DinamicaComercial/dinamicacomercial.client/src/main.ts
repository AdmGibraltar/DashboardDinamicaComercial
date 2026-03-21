import { createApp } from 'vue'
import App from './App.vue'
import './index.css'
import icons from './core/icons/icons'
import { createPinia } from 'pinia'

const app = createApp(App)
const pinia = createPinia()
app.use(pinia)

import { VueDatePicker } from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'

app.component('font-awesome-icon', icons)
app.component('VueDatePicker', VueDatePicker)
app.mount('#app')
