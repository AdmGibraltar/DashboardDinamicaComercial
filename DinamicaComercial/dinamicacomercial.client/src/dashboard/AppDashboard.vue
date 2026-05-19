<template>
    <div class="dashboard-content">
        <KpiFilters />

        <div class="container-fluid px-5 py-4">

            <div v-if="showAlert" class="alert alert-info alert-dismissible fade show shadow-sm mb-4" role="alert">
                <FontAwesomeIcon :icon="['fas', 'info-circle']" class="me-2" />
                <strong>¡Atención!</strong> Tienes motivos de decremento pendientes por capturar.
                <button
                    type="button"
                    class="btn-close"
                    @click="showAlert = false"
                    aria-label="Close">
                </button>
            </div>

            <div v-if="store.isInvalidMonth" class="alert alert-warning shadow-sm mb-4">
                <FontAwesomeIcon :icon="['fas', 'exclamation-triangle']" class="me-2" />
                El mes de <strong>{{ store.confirmedMonthName }}</strong> se encuentra en curso.
                Solo se puede visualizar información de meses cerrados (<strong>{{ store.confirmedPreviousMonthName }}</strong> y anteriores).
            </div>
            <div v-else class="row g-4">
                <div class="col-12">
                    <template v-if="store.submitted">
                        <Kpi1Section @scroll-to-kpi2="scrollToKpi2" />
                        <div ref="kpi2Ref" style="scroll-margin-top: 100px;">
                            <Kpi2Section />
                        </div>
                        <Kpi3Section />
                    </template>

                    <div v-else-if="!store.submitted && authStore.isSIANCENTRALMode" class="text-center py-5 opacity-50">
                        <FontAwesomeIcon :icon="['fas', 'filter']" size="3x" class="mb-3" />
                        <h3>Bienvenido</h3>
                        <p>Seleccione los filtros y haga clic en aplicar para comenzar.</p>
                    </div>
                </div>
            </div>
        </div>

        <Transition name="fade">
            <button title="Ir arriba" @click="scrollToTop" v-show="showScrollTop" type="button"
                class="scroll-to-top-btn">
                <FontAwesomeIcon :icon="['fas', 'chevron-up']" />
            </button>
        </Transition>
    </div>
</template>

<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import KpiFilters from './components/KpiFilters/KpiFilters.vue'
import Kpi1Section from './components/KPI1/Kpi1Section.vue'
import Kpi2Section from './components/KPI2/Kpi2Section.vue'
import Kpi3Section from './components/KPI3/Kpi3Section.vue'
import { useDashboardStore } from './store/dashboardStore'
import { useScrollToTop } from './composables/useScrollToTop'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { useAuthStore } from '@/core/store/authStore'
import { useDashboard } from './composables/useDashboard'

const store = useDashboardStore()
const authStore = useAuthStore()
const { showScrollTop, scrollToTop } = useScrollToTop()
const { initCatalogs, getDecrementoVentas } = useDashboard()
const kpi2Ref = ref<HTMLDivElement | null>(null)
const showAlert = ref<boolean>(false)


const scrollToKpi2 = (): void => {
    if (kpi2Ref.value) {
        kpi2Ref.value.scrollIntoView({
            block: 'start',
            behavior: 'smooth'
        })
    }
}

onMounted(async () => {
    if (!authStore.isSIANCENTRALMode) {
        store.filters.sucursal = [{ id: authStore.sucursalId, name: '' }]

        await initCatalogs()
        await store.fetchData()
        if (store.data?.isClosingMonth) {
            const rikId = authStore.isGerente ? undefined : store.filters.rik ?? undefined
            await getDecrementoVentas(rikId)
        }
    }
})

watch(
    () => store.decrementoVentas,
    (newValue) => {
        showAlert.value = newValue && newValue.some(x => x.idMotivo === null && x.idCausa === null)
    }, { immediate: true }
)
</script>

<style scoped src="./styles.css"></style>
