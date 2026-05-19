import type { catalogCausaDecremento, catalogs } from '@/dashboard/types/catalogs'
import { apiService } from '@/core/services/api-service'

export const catalogsService = {
    getRiks: async (sucursalId: number, isGerente: boolean, idUser?: number): Promise<catalogs[]> => {
        try {
            let url = `riks?sucursalId=${sucursalId}&isGerente=${isGerente}`
            if (idUser) url += `&idUser=${idUser}`

            const rikResponse = await apiService.fetch(url)
            return rikResponse.ok ? await rikResponse.json() as catalogs[] : []
        } catch { return [] }
    },

    getUens: async (sucursalId: number): Promise<catalogs[]> => {
        try {
            const url = 'uens?sucursalId=' + sucursalId
            const uensResponse = await apiService.fetch(url)
            return uensResponse.ok ? await uensResponse.json() as catalogs[] : []
        } catch { return [] }
    },

    getMotivosDecremento: async (): Promise<catalogs[]> => {
        try {
            const url = 'motivos-decremento'
            const response = await apiService.fetch(url)
            return response.ok ? await response.json() as catalogs[] : []
        } catch { return [] }
    },

    getCausasDecremento: async (): Promise<catalogCausaDecremento[]> => {
        try {
            const url = 'causas-decremento'
            const response = await apiService.fetch(url)
            return response.ok ? await response.json() as catalogCausaDecremento[] : []
        } catch { return [] }
    }
}