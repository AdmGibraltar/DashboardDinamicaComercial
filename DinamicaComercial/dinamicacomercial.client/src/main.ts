import { createApp } from 'vue'
import App from './App.vue'
import './index.css'
import icons from './core/icons/icons'
import { createPinia } from 'pinia'
import { VueDatePicker } from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'

const app = createApp(App)

const pinia = createPinia()

app.use(pinia)
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        
    }
})

app.component('font-awesome-icon', icons)
app.component('VueDatePicker', VueDatePicker)
app.mount('#app')
