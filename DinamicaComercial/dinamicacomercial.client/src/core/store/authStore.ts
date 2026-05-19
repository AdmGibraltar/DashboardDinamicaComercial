import { defineStore } from 'pinia'
import type { Session } from '../types/session'

interface LoginData {
	userId: number | null
	userName: string | null
	idCd: number | null
	role?: 'rik' | 'gte' | null
}

interface AuthState {
	loginData: LoginData
	isLoading: boolean
	isSIANCENTRAL: boolean
}

export const useAuthStore = defineStore('auth', {
	state: (): AuthState => ({
		loginData: {
			userId: null,
			userName: null,
			idCd: null
		},
		isLoading: true,
		isSIANCENTRAL: false
	}),
	getters: {
		isLoggedIn: (state) => !!state.loginData.userId && !!state.loginData.idCd,
		sucursalId: (state) => state.loginData.idCd ?? 0,
		isGerente: state => !state.isSIANCENTRAL && state.loginData.role === 'gte',
		isSIANCENTRALMode: state => state.isSIANCENTRAL
	},
	actions: {
		setLoginData(data: Session) {
			const isLogged = data && data.loggedIn
			
			this.loginData.userId = isLogged ? data.userId : null
			this.loginData.userName = isLogged ? data.userName : null
			this.loginData.idCd = isLogged ? data.cd : null
			this.loginData.role = isLogged ? data.role : null
			this.isLoading = false

			this.isSIANCENTRAL = data.isSIANCENTRAL || false
		},
		setLoading(isLoading: boolean): void {
			this.isLoading = isLoading
		}
	}
})