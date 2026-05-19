export interface Metrics {
    isClosingMonth: boolean,
    kpi1: Kpi1[],
    kpi2: Kpi2[],
    kpi3: Kpi3[]
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

interface Kpi2 {
    segmento: string
    clientes: number
    clientes6M: number
    totalVentas: number
    totalVentas6M: number
    crecimientoAbsoluto: number
    crecimientoPorcentual: number
    porcentajeVentas: number
}

export interface Kpi3 {
    causa: string
    motivo: string
    totalClientes: number
    totalVentaMes: number
    totalPromedio6M: number
    totalDiferencia: number
    porcentajeDecremento: number
}