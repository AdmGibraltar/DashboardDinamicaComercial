import { useAuthStore } from '@/core/store/authStore'
import { useDashboardStore } from '../store/dashboardStore'
import type { DecrementoVentas } from '../types/decrementoVentas'
import type { Kpi3 } from '../types/metrics'
import { catalogsService } from '@/shared/services/catalogs-service'
import { apiService } from '@/core/services/api-service'
import { computed } from 'vue'

export function useDashboard(): {
    initCatalogs: () => Promise<void>
    getRiks: () => Promise<void>
    getUens: () => Promise<void>
    getMotivosDecremento: () => Promise<void>
    getCausasDecremento: () => Promise<void>
    getDecrementoVentas: (rikId?: number) => Promise<void>
    getDashOnlyKpi3: () => Promise<void>
} {
    const store = useDashboardStore()
    const authStore = useAuthStore()

    const sucursalId = computed(() => (authStore.isSIANCENTRALMode ? store.filters.sucursal?.[0]?.id : authStore.sucursalId) ?? 0)

    const initCatalogs = async () => {
        store.catalogs.riks = []
        store.catalogs.uens = []
        store.catalogs.motivosDecremento = []
        store.catalogs.causasDecremento = []

        await Promise.all([
            getRiks(),
            getUens(),
            getMotivosDecremento(),
            getCausasDecremento()
        ])
    }

    const getRiks = async () => {
        const isGerente = authStore.isSIANCENTRALMode ? true : authStore.isGerente
        const userId = authStore.isSIANCENTRALMode ? undefined : (authStore.loginData.userId ?? undefined)

        const riks = await catalogsService.getRiks(sucursalId.value, isGerente, userId)
        store.catalogs.riks = riks

        if (riks && riks.length > 0) {
            if (!authStore.isSIANCENTRALMode && !authStore.isGerente || riks.length === 1) {
                store.filters.rik = riks[0]?.id ?? null
            } else store.filters.rik = null
        }
    }

    const getUens = async () => {
        const uens = await catalogsService.getUens(sucursalId.value)
        store.catalogs.uens = uens
    }

    const getMotivosDecremento = async () => {
        const motivos = await catalogsService.getMotivosDecremento()
        store.catalogs.motivosDecremento = motivos
    }

    const getCausasDecremento = async () => {
        const causas = await catalogsService.getCausasDecremento()
        store.catalogs.causasDecremento = causas
    }

    const getDecrementoVentas = async (rikId?: number) => {
        try {
            let url = `decremento-ventas?sucursalId=${sucursalId.value}`
            if (rikId) url += `&rikId=${rikId}`

            const res = await apiService.fetch(url)
            if (res.ok) store.decrementoVentas = await res.json() as DecrementoVentas[]
        } catch { store.decrementoVentas = [] }
    }

    const getDashOnlyKpi3 = async () => {
        let url = `metrics-kpi3?sucursalId=${sucursalId.value}`

        const { year, month } = store.filters.date
        url += `&year=${year}&month=${month + 1}`

        if (store.filters.rik) url += `&rikId=${store.filters.rik}`

        const res = await apiService.fetch(url)
        if (res.ok) {
            const data = await res.json() as Kpi3[]
            if (data && store.data) store.data.kpi3 = data
        }
    }



    return {
        initCatalogs,
        getRiks,
        getUens,
        getMotivosDecremento,
        getCausasDecremento,
        getDecrementoVentas,
        getDashOnlyKpi3
    }
}
