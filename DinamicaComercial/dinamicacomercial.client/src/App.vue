<template>
	<div class="min-vh-100 bg-light-subtle">
		<AppHeader />
		<main>
			<AppDashboard />
		</main>
	</div>
</template>

<script setup lang="ts">
import AppHeader from './shared/layout/AppHeader.vue'
import AppDashboard from './dashboard/AppDashboard.vue'
</script>