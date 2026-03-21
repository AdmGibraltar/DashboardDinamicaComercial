export interface Metrics {
    kpi1: Kpi1[]
}

export interface Kpi1 {
    segmento: string
    clientes: number
    clientes6M: number
    totalVentas: number
    totalVentas6M: number
    crecimientoAbsoluto: number
    crecimientoPorcentual: number
}