export interface Session {
    loggedIn: boolean
    userId: number
    userName: string
    role: 'rik' | 'gte' | null
    cd: number | null
    isSIANCENTRAL?: boolean
    description?: string
}