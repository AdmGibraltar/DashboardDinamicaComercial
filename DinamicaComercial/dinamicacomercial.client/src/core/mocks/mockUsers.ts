import type { Session } from '../types/session'

export type UserRole = 'rik' | 'gte'
export const MOCK_USERS: Record<UserRole, Session> = {
    rik: {
        loggedIn: true,
        userId: 1140,
        userName: 'Test',
        cd: 110,
        role: 'rik',
        description: 'username: LYHER / password: 321'
    },
    gte: {
        loggedIn: true,
        userId: 1008,
        userName: 'Administrador',
        cd: 110,
        role: 'gte',
        description: ''
    }
}

export const getMockUser = (role: UserRole): Session => {
    return MOCK_USERS[role]
}
