<template>
    <div>
        <label for="riks" class="filter-label">Sucursales</label>
        <MultiSelect 
            v-model="store.filters.sucursal" 
            :options="sucursales" 
            optionLabel="name"
            filter
            :max-selected-labels="1"
            selected-items-label="{0} sucursales seleccionadas"
            placeholder="Selecciona sucursales" 
            emptyMessage="No hay sucursales disponibles"
            emptyFilterMessage="No se encontraron resultados"
            class="custom-input w-full" 
        />
    </div>
</template>

<script setup lang="ts">
import { useDashboardStore } from '@/dashboard/store/dashboardStore'
import type { catalogs } from '@/dashboard/types/catalogs'
import { ref, watch } from 'vue'
import MultiSelect from 'primevue/multiselect'
import { apiService } from '@/core/services/api-service'

const store = useDashboardStore()
const sucursales = ref<catalogs[]>([])

const getSucursales = async (sucursalGroupId: number) => {
    try {
        const sucursalesResponse = await apiService.fetch(`sucursales?groupId=${sucursalGroupId}`)
        if (sucursalesResponse.ok) sucursales.value = await sucursalesResponse.json() as catalogs[]
    } catch {}
}

watch(
    () => store.filters.sucursalGroup,
    async (newValue) => {
        sucursales.value = []
        store.filters.sucursal = [] 
        
        if (newValue !== null && newValue !== undefined) getSucursales(newValue)
    },
    { immediate: true }
)
</script>

<style scoped>
.form-select-sm {
    padding-right: 0.25rem;
}
:deep(.p-multiselect) {
    display: flex;
    width: 100% !important;
    align-items: center;
    line-height: 1;
    min-height: 35px;
    height: 35px;
}
:deep(.p-multiselect-label) {
    padding: 0 0.75rem !important;
    display: flex;
    align-items: center;
    font-size: 0.85rem;
    color: #1e293b !important;
}
:deep(.p-multiselect-filter-container .p-inputtext) {
    font-size: 0.75rem !important;
    padding: 0.4rem 0.6rem;
}
:deep(.p-multiselect-option) {
    font-size: 0.8rem;
    padding: 0.5rem 0.75rem;
}
:deep(.p-multiselect-dropdown) {
    width: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
}
:deep(.p-multiselect-label-container) {
    width: 100%;
}
</style>