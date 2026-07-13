import axios from 'axios'
import { getMockUser, type UserRole } from '../mocks/mockUsers'
import type { Session } from '../types/session'

/**
 * Verifica el inicio de sesión en Sianweb.
 * En producción, hace una solicitud al backend para obtener la sesión actual.
 * En desarrollo, devuelve un usuario simulado basado en la variable de entorno VITE_MOCK_USER.
 * @returns
 */
export const loginService = {
    verifyLogin: async (): Promise<Session> => {
        if (import.meta.env.MODE === 'production') {
            const url = getAuthUrl()
            const response = await axios.get<Session>(url)

            if (url.toLowerCase().includes('siancentral')) {
                response.data.isSIANCENTRAL = true
            }

            return response.data
        }
        else {
            console.log('Running in development mode, returning mock user.')
            const mockUserId = (import.meta.env.VITE_MOCK_USER || 'rik') as UserRole
            const user = getMockUser(mockUserId as UserRole)
            user.isSIANCENTRAL = import.meta.env.VITE_IS_SIANCENTRAL === 'true'

            return user
        }
    }
}

const getAuthUrl = () => {
    const STORAGE_KEY = 'sian_parent_app'
    const referrer = document.referrer
    let activeApp = ''

    if (referrer) {
        try {
            if (referrer.toLowerCase().includes('https://siancentral.sianweb.com.mx') || referrer.toLowerCase().includes('http://40.84.229.61')) {
                activeApp = 'siancentral'
                sessionStorage.setItem(STORAGE_KEY, activeApp)
                return `${referrer}siancentral/externalAuth.ashx`
            }
            const url = new URL(referrer)
            const pathParts = url.pathname.split('/').filter(p => p !== '')
            if (pathParts.length > 0) {
                activeApp = pathParts[0] ?? ''
                sessionStorage.setItem(STORAGE_KEY, activeApp)
            }
        } catch {}
    }

    activeApp = activeApp || sessionStorage.getItem(STORAGE_KEY) || 'sianweb'
    return `/${activeApp}/externalAuth.ashx`
}
