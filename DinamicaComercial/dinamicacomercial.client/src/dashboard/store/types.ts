import type { catalogCausaDecremento, catalogs } from '../types/catalogs'
import type { DecrementoVentas } from '../types/decrementoVentas'
import type { Metrics } from '../types/metrics'

export interface DashboardState {
    filters: DashoardFilters
    data: Metrics | null
    catalogs: {
        riks: catalogs[]
        uens: catalogs[]
        motivosDecremento: catalogs[]
        causasDecremento: catalogCausaDecremento[]
    }
    submitted: boolean
    isInvalidMonth: boolean
    confirmedMonthName?: string
    confirmedPreviousMonthName?: string
    formMotivoDecremento: FormDecrementoVentas[]
    decrementoVentas: DecrementoVentas[]
}

interface DashoardFilters {
    date: {
        year: number
        month: number
    }
    rik: number | null
    uen: number | null
    segmento: number | null
    sucursalGroup: number | null
    sucursal: catalogs[] | null
}

export interface FormDecrementoVentas {
    motivo: number | null
    causa: number | null
}
