<template>
	<section id="kpi3" class="mb-5">
		<div class="d-flex align-items-center justify-content-between mb-4">
			<div class="d-flex align-items-center">
				<div class="custom-indicator me-2"></div>
				<h5 class="fw-bold mb-0 text-dark">Motivos de Decremento en Venta</h5>
			</div>
			<DecrementoVentas />
		</div>

		<div class="row g-4">
			<div class="col-12 col-xl-5">
				<div class="card border-0 shadow-sm h-100 bg-white">
					<div class="card-body d-flex flex-column align-items-center justify-content-center p-4">
						<h6 class="text-muted fw-bold text-uppercase x-small mb-4 tracking-wider">
							Distribución por Motivo (Actual)
						</h6>

						<div class="chart-container-pie" style="position: relative; height: 250px; width: 100%;">
							<canvas ref="chartCanvas"></canvas>
						</div>

						<div class="d-flex flex-wrap justify-content-center mt-4 gap-3">
							<div v-for="item in processedData" :key="item.nombre" class="d-flex align-items-center">
								<div class="status-dot-sm me-1" :style="{ backgroundColor: item.color }"></div>
								<span class="x-small text-muted">{{ item.nombre }}</span>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="col-12 col-xl-7">
				<div class="card border-0 shadow-sm table-card bg-white">
					<div class="card-header bg-transparent py-3 border-bottom-light">
						<h6 class="mb-0 fw-bold text-dark x-small text-uppercase tracking-wider">
							Desglose de Causas
						</h6>
					</div>
					<div class="card-body p-0">
						<div class="table-responsive">
							<table class="table table-hover align-middle mb-0">
								<thead>
									<tr class="x-small text-muted text-uppercase">
										<th class="ps-4 py-3 border-0">Motivo / Causa</th>
                                        <th class="border-0 text-center">Mes Actual</th>
										<th class="border-0 text-center">Semestre Pasado (Prom)</th>
										<th class="border-0 text-center">Variación</th>
										<th class="border-0 text-end pe-4">Var. %</th>
									</tr>
								</thead>
								<tbody v-for="motivo in processedData" :key="motivo.nombre">
									<tr class="bg-light bg-opacity-25">
										<td colspan="5" class="ps-4 py-2">
											<div class="d-flex align-items-center">
												<div class="status-dot me-2" :style="{ backgroundColor: motivo.color }">
												</div>
												<span class="fw-bold text-dark small text-uppercase">
													{{ motivo.nombre }}
													<span class="text-muted fw-normal">({{ motivo.totalClientes }} clientes)</span>
												</span>
											</div>
										</td>
									</tr>
									<tr v-for="causa in motivo.causas" :key="causa.descripcion">
										<td class="ps-5">
											<span class="text-muted small">- {{ causa.descripcion }} ({{ causa.clientes }})</span>
										</td>
                                        <td class="text-center small fw-bold text-dark">
											${{ causa.promedioActual.toLocaleString() }}
										</td>
										<td class="text-center small text-secondary">
											${{ causa.promedioPasado.toLocaleString() }}
										</td>
										<td
											:class="['text-center small fw-bold', causa.diferencia < 0 ? 'text-danger' : 'text-success']">
											${{ causa.diferencia.toLocaleString() }}
										</td>
										<td class="text-end pe-4">
											<span :class="[
												causa.variacion > 0 ? 'text-success' : 'text-danger',
												'badge rounded-pill fw-bold x-small border'
											]">
												{{ causa.variacion > 0 ? '+' : '' }}{{ causa.variacion.toFixed(1) }}%
											</span>
										</td>
									</tr>
								</tbody>

								<tfoot v-if="processedData.length > 0" class="bg-light border-top-2">
									<tr class="fw-bold text-dark">
										<td class="ps-4 py-3">TOTAL GENERAL</td>
                                        <td class="text-center">
											${{ grandTotals.actual.toLocaleString() }}
										</td>
										<td class="text-center">
											${{ grandTotals.pasado.toLocaleString() }}
										</td>
										<td
											:class="['text-center', grandTotals.diferencia < 0 ? 'text-danger' : 'text-success']">
											${{ grandTotals.diferencia.toLocaleString() }}
										</td>
										<td class="text-end pe-4">
											<span
												:class="['badge rounded-pill', grandTotals.variacion > 0 ? 'bg-success' : 'bg-danger']">
												{{ grandTotals.variacion > 0 ? '+' : '' }}{{ grandTotals.variacion.toFixed(1) }}%
											</span>
										</td>
									</tr>
								</tfoot>

								<tfoot v-else>
									<tr>
										<td colspan="5" class="text-center py-4 text-muted small">No hay datos registrados</td>
									</tr>
								</tfoot>
							</table>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<script setup lang="ts">
import { ref, computed, watch, nextTick, onUnmounted } from 'vue'
import Chart from 'chart.js/auto'
import DecrementoVentas from './decrementoVentas/DecrementoVentas.vue'
import { useDashboardStore } from '@/dashboard/store/dashboardStore'

const store = useDashboardStore()

const colorMap: Record<string, string> = {
    'Key': '#0ea5e9',
    'Cliente': '#14b8a6',
    'No. Pérdida': '#0369a1',
    'Default': '#94a3b8'
}

interface ProcesoMotivo {
	nombre: string
	color: string
	totalClientes: number
	causas: {
		descripcion: string
        clientes: number
		promedioPasado: number
		promedioActual: number
		diferencia: number
		variacion: number
	}[]
}

const processedData = computed(() => {
    const rawData = store.data?.kpi3 || []
    const groups: Record<string, ProcesoMotivo> = {}

    rawData.forEach(item => {
        const motivoNombre = item.motivo || 'Sin Clasificar'

        if (!groups[motivoNombre]) {
            groups[motivoNombre] = {
                nombre: motivoNombre,
                color: colorMap[motivoNombre] || colorMap['Default'] || '#6c757d',
                totalClientes: 0,
                causas: []
            }
        }

        groups[motivoNombre].totalClientes += (item.totalClientes || 0)

        groups[motivoNombre].causas.push({
            descripcion: item.causa,
            clientes: item.totalClientes,
            promedioPasado: item.totalPromedio6M,
            promedioActual: item.totalVentaMes,
			diferencia: item.totalDiferencia,
            variacion: item.porcentajeDecremento
        })
    })

    return Object.values(groups)
})

const chartCanvas = ref<HTMLCanvasElement | null>(null)
let chartInstance: Chart | null = null

const updateChart = () => {
    if (!chartCanvas.value) return

    if (chartInstance) {
        chartInstance.destroy()
        chartInstance = null
    }

    const hasData = processedData.value.length > 0

    const labels = hasData ? processedData.value.map(m => m.nombre) : ['Sin datos']
    const dataValues = hasData
        ? processedData.value.map(m => m.causas.reduce((acc, c) => acc + c.promedioActual, 0))
        : [1]
    const colors = hasData ? processedData.value.map(m => m.color) : ['#e2e8f0']

    chartInstance = new Chart(chartCanvas.value, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: dataValues,
                backgroundColor: colors,
                borderWidth: 2,
                borderColor: '#ffffff'
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false },
                tooltip: {
                    callbacks: {
                        label: (context) => hasData ? ` Total: $${context.parsed.toLocaleString()}` : 'Sin datos disponibles'
                    }
                }
            }
        }
    })
}

const grandTotals = computed(() => {
    const totals = {
        pasado: 0,
        actual: 0,
        diferencia: 0,
        variacion: 0
    }

    processedData.value.forEach(motivo => {
        motivo.causas.forEach(causa => {
            totals.pasado += causa.promedioPasado
            totals.actual += causa.promedioActual
            totals.diferencia += causa.diferencia
        })
    })

    if (totals.pasado !== 0) {
        totals.variacion = ((totals.actual - totals.pasado) / totals.pasado) * 100
    }

    return totals
})

watch(() => processedData.value, async () => {
    await nextTick()
    updateChart()
}, { deep: true, immediate: true })

onUnmounted(() => chartInstance?.destroy())
</script>

<style scoped src="./styles.css"></style>
