import { defineStore } from 'pinia'
import type { Metrics } from '../types/metrics'
import { useAlert } from '@/core/composables/useAlert'

interface DashboardState {
    filters: {
        date: {
            year: number
            month: number
        }
    },
    data: Metrics | null
}
export const useDashboardStore = defineStore('dashboard', {
    state: (): DashboardState => ({
        filters: {
            date: {
                year: new Date().getFullYear(),
                month: new Date().getMonth()
            }
        },
        data: null
    }),
    actions: {
        async fetchData() {
            const { loading, close } = useAlert()
            loading('Cargando datos...')

            const { year, month } = this.filters.date
            const parameters = new URLSearchParams({ 
                year: year.toString(),
                month: (month + 1).toString()
            })

            const response = await fetch(`metrics?${parameters.toString()}`)
            if (response.ok) {
                const data = (await response.json()) as Metrics
                this.data = data
            }
            close()
        }
    }
})