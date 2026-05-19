<template>
    <div>
        <label for="riks" class="filter-label">CDI/CDC</label>
        <select
            v-model="store.filters.sucursalGroup"
            class="form-select form-select-sm custom-input"
            id="riks"
        >
            <option :value="null">Todos los CDIs/CDCs</option>
            <option :value="s.id" v-for="s in sucursales" :key="s.id">
                {{ s.name }}
            </option>
        </select>
    </div>
</template>

<script setup lang="ts">
import { useDashboardStore } from '@/dashboard/store/dashboardStore'
import type { catalogs } from '@/dashboard/types/catalogs'
import { onMounted, ref } from 'vue'
import { apiService } from '@/core/services/api-service'

const store = useDashboardStore()
const sucursales = ref<catalogs[]>([])

const getSucursalesGroup = async () => {
    try {
        const sucursalesGroupResponse = await apiService.fetch('sucursales-group')

        if (sucursalesGroupResponse.ok) sucursales.value = await sucursalesGroupResponse.json() as catalogs[]
    } catch {}
}

onMounted(async () => await getSucursalesGroup())
</script>