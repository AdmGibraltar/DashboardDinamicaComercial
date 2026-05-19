import { defineStore } from 'pinia'
import type { Metrics } from '../types/metrics'
import { useAlert } from '@/core/composables/useAlert'
import { useAuthStore } from '@/core/store/authStore'
import { loginService } from '@/core/services/login-service'
import { formatMonth } from '@/core/utils/textFormat'
import type { DashboardState } from './types'
import { apiService } from '@/core/services/api-service'

export const useDashboardStore = defineStore('dashboard', {
    state: (): DashboardState => ({
        filters: {
            date: {
                year: new Date().getFullYear(),
                month: new Date().getMonth() -1
            },
            rik: null,
            uen: null,
            segmento: null,
            sucursalGroup: null,
            sucursal: []
        },
        data: null,
        catalogs: {
            riks: [],
            uens: [],
            motivosDecremento: [],
            causasDecremento: []
        },
        submitted: false,
        isInvalidMonth: false,
        confirmedMonthName: '',
        confirmedPreviousMonthName: '',
        formMotivoDecremento: [],
        decrementoVentas: []
    }),
    actions: {
        async fetchData(fromFilters: boolean = false): Promise<void> {
            const { loading, close } = useAlert()
            const authStore = useAuthStore()

            if (!this.isPeriodAllowed()) return

            if (authStore.isSIANCENTRALMode && (!this.filters.sucursal || this.filters.sucursal.length === 0)) {
                this.submitted = true
                this.data = null
                return
            }

            if (fromFilters) {
                const sesion = await loginService.verifyLogin()
                authStore.setLoginData(sesion)

                if (!authStore.isLoggedIn) return
            }

            try {
                loading('Cargando datos...')

                this.submitted = true
                const parameters = this.getParameters()
                const url = `metrics?${parameters.toString()}`
                const response = await apiService.fetch(url)

                if (response.ok) this.data = (await response.json()) as Metrics
                else this.data = null
            }
            catch {}
            finally { close() }
        },

        getParameters(): URLSearchParams {
            const authStore = useAuthStore()

            const { year, month } = this.filters.date
            const parameters = new URLSearchParams({
                year: year.toString(),
                month: (month + 1).toString()
            })

            if (this.filters.rik) parameters.append('rikId', this.filters.rik.toString())
            if (this.filters.uen) parameters.append('idUen', this.filters.uen.toString())
            if (this.filters.segmento) parameters.append('idSeg', this.filters.segmento.toString())
            if (authStore.isSIANCENTRALMode && this.filters.sucursal?.length) {
                const ids = this.filters.sucursal.map(s => s.id).join(',')
                parameters.append('sucursalId', ids)
            } else parameters.append('sucursalId', authStore.sucursalId.toString())

            return parameters
        },

        isPeriodAllowed(): boolean {
            const currentDate = new Date()
            const selectedDate = new Date(this.filters.date.year, this.filters.date.month)
            const isCurrent = selectedDate.getFullYear() === currentDate.getFullYear() && selectedDate.getMonth() === currentDate.getMonth()
            if (isCurrent) {
                this.isInvalidMonth = true
                this.submitted = true
                this.data = null
                this.confirmedMonthName = formatMonth(selectedDate)

                const previousMonthDate = new Date(selectedDate.getFullYear(), selectedDate.getMonth() - 1)
                previousMonthDate.setDate(1)
                this.confirmedPreviousMonthName = formatMonth(previousMonthDate)
                return false
            } else {
                this.isInvalidMonth = false
                this.confirmedMonthName = ''
                this.confirmedPreviousMonthName = ''
            }

            return true
        }
    }
})
