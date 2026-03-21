<template>
    <div class="dashboard-content">
        <AppFilters />
        <div class="container-fluid px-5 py-4">
            <div class="row g-4">
                <div class="col-12">
                    <Kpi1Section />
                    <Kpi2Section />
                    <Kpi3Section />
                    <Kpi4Section />
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import AppFilters from './components/AppFilters.vue'
import Kpi1Section from './components/KPI1/Kpi1Section.vue'
import Kpi2Section from './components/KPI2/Kpi2Section.vue'
import Kpi3Section from './components/KPI3/Kpi3Section.vue'
import Kpi4Section from './components/KPI4/Kpi4Section.vue'
import { useDashboardStore } from './store/dashboardStore'

const store = useDashboardStore()

onMounted(async () => await store.fetchData())
</script>

<style scoped>
.dashboard-content {
    min-height: calc(100vh - 70px); 
    display: flex;
    flex-direction: column;
}
.container-fluid {
    animation: fadeIn 0.5s ease-in-out;
}
</style>
