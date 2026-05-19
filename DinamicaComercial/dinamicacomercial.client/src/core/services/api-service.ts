import { useAuthStore } from '../store/authStore'

export const apiService = {
    fetch: async (endpoint: string, options?: RequestInit): Promise<Response> => {
        const authStore = useAuthStore()

        const headers: Record<string, string> = {
            'Content-Type': 'application/json',
            'mode': authStore.isSIANCENTRALMode ? 'siancentral' : 'sianweb'
        }

        if (!authStore.isSIANCENTRALMode) {
            headers['sucursal-id'] = authStore.sucursalId.toString()
        }

        const config: RequestInit = {
            ...options,
            headers: {
                ...headers,
                ...options?.headers
            }
        }

        const isProd = import.meta.env.MODE === 'production'
        const apiBase = isProd ? `/${import.meta.env.VITE_IIS_APP_NAME}/api` : '/api'
        const response = await fetch(`${apiBase}/${endpoint}`, config)

        return response
    }
}