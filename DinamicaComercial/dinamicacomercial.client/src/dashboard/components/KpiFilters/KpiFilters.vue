<template>
    <div class="filter-bar shadow-sm">
        <div class="container-fluid px-5">
            <div class="row g-3 row-gap-3 align-items-end">
                <div v-if="authStore.isSIANCENTRALMode" class="col-6 col-md-2">
                    <KpiFiltersSucursalesGroup />
                </div>

                <div v-if="authStore.isSIANCENTRALMode" class="col-6 col-md-2">
                    <KpiFilterSucursales />
                </div>
            </div>
            <div class="row mt-2 row-gap-3 align-items-end" :class="{ 'g-3': !authStore.isSIANCENTRALMode }">
                <div class="col-6 col-md-2">
                    <label for="periodo" class="filter-label">Periodo</label>
                    <VueDatePicker
                        v-model="store.filters.date"
                        :formats="{ input: 'MMMM yyyy', preview: '---' }"
                        month-picker
                        auto-apply
                        :max-date="new Date()"
                        :locale="es"
                        class="custom-datepicker"
                    />
                </div>

                <div class="col-6 col-md-2">
                    <KpiFiltersRiks :disabled="isMultipleSucursalSelected" />
                </div>

                <div class="col-6 col-md-2">
                    <KpiFiltersUen :disabled="isMultipleSucursalSelected" />
                </div>

                <div class="col-6 col-md-2">
                    <KpiFiltersSegmentos :disabled="isMultipleSucursalSelected" />
                </div>

                <div class="col-12 col-md-auto ms-md-auto">
                    <button @click="store.fetchData(true)"
                        class="btn btn-primary btn-sm px-4 rounded-pill fw-bold shadow-sm w-100">
                        <FontAwesomeIcon icon="fa-filter" class="me-2" />
                        Aplicar
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { es } from 'date-fns/locale'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { useDashboardStore } from '../../store/dashboardStore'
import KpiFiltersRiks from './KpiFiltersRiks.vue'
import KpiFiltersUen from './KpiFiltersUen.vue'
import KpiFiltersSegmentos from './KpiFiltersSegmentos.vue'
import KpiFiltersSucursalesGroup from './KpiFiltersSucursalesGroup.vue'
import KpiFilterSucursales from './KpiFilterSucursales.vue'
import { useAuthStore } from '@/core/store/authStore'
import { computed, watch } from 'vue'
import { useDashboard } from '@/dashboard/composables/useDashboard'

const store = useDashboardStore()
const authStore = useAuthStore()
const { getUens, getRiks } = useDashboard()

const isMultipleSucursalSelected = computed(() => {
    const selectedCount = store.filters.sucursal?.length || 0
    return selectedCount !== 1
})

watch(
    [() => store.filters.sucursalGroup, () => store.filters.sucursal],
    async ([newGroup, newSucursal]) => {
        store.catalogs.uens = []
        store.filters.uen = null
        store.catalogs.riks = []
        store.filters.rik = null

        const hasSelectedGroup = newGroup !== null && newGroup !== undefined
        const hasExactlyOneSucursal = Array.isArray(newSucursal) && newSucursal.length === 1

        if (hasSelectedGroup && hasExactlyOneSucursal) {
            await getUens()
            await getRiks()
        }
    },
    { deep: true }
)
</script>

<style scoped src="./styles.css"></style>