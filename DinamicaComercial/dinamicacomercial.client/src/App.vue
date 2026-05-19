<template>
	<PageLoading v-if="authStore.isLoading" />

	<div v-else class="min-vh-100" style="background-color: #f4f7f9;">
		<AppHeader />
		<main>
			<AppDashboard v-if="authStore.isLoggedIn" />
			<NoAutorizado v-else />
		</main>
	</div>
</template>

<script setup lang="ts">
import AppHeader from './shared/layout/AppHeader.vue'
import AppDashboard from './dashboard/AppDashboard.vue'
import { onMounted, } from 'vue'
import { loginService } from './core/services/login-service'
import { useAuthStore } from './core/store/authStore'
import NoAutorizado from './core/views/NoAutorizado.vue'
import PageLoading from './core/views/PageLoading.vue'

const authStore = useAuthStore()

onMounted(async () => {
	authStore.setLoading(true)
	authStore.$reset()

	try {	
		const sesion = await loginService.verifyLogin()
		authStore.setLoginData(sesion)
	}
	catch {}
	finally { authStore.setLoading(false) }
})
</script>