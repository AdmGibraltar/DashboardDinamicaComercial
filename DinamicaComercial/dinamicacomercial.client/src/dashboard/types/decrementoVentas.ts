export interface DecrementoVentas {
    id: number
    idRik: number
    rik: string
    idCliente: number
    cliente: string
    ventaMes: number
    promedioVenta6M: number
    porcentajeDecremento: number
    idMotivo: number | null
    idCausa: number | null
    completado: boolean
}