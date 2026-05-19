<template>
	<section id="kpi2" class="mb-5">
		<div class="d-flex align-items-center mb-4">
			<div class="custom-indicator me-2"></div>
			<h5 class="fw-bold mb-0 text-dark">Comportamiento de Recurrencia</h5>
		</div>

		<div class="row g-4">
			<div class="col-12 col-xl-5">
				<div class="card border-0 shadow-sm h-100 bg-white">
					<div class="card-body d-flex flex-column align-items-center justify-content-center p-4">
						<h6 class="text-muted fw-bold text-uppercase x-small mb-4 tracking-wider">
							Distribución de ventas
						</h6>

						<div class="chart-container">
							<template v-if="hasData">
								<canvas ref="chartCanvas"></canvas>
								<div class="chart-center-text">
									<span class="fw-bold text-dark fs-4">100%</span>
								</div>
							</template>
							<div v-else class="empty-chart-placeholder d-flex flex-column align-items-center justify-content-center">
								<div class="empty-circle"></div>
								<div class="chart-center-text">
									<span class="text-muted small">Sin datos</span>
								</div>
							</div>
						</div>

						<div class="d-flex flex-wrap justify-content-center mt-4 gap-3">
							<div v-for="item in kpiMetrics" :key="item.segmento" class="d-flex align-items-center">
								<div class="status-dot-sm me-1" :style="{ backgroundColor: item.color }"></div>
								<span class="x-small text-muted">
                                    {{ item.segmento }} ({{ Number(item.porcentajeVentas).toFixed(1) }}%)
                                </span>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="col-12 col-xl-7">
				<div class="row g-3">

					<div class="col-12">
						<div class="card card-kpi-featured shadow-sm bg-white py-2">
							<div class="card-body px-4">
								<div class="row align-items-center text-center text-md-start">
									<div class="col-md-5 border-end-md">
										<span class="x-small fw-bold text-muted text-uppercase d-block mb-1">Ingreso actual</span>
										<h2 class="fw-bold mb-0 text-blue-accent">${{ kpi1Metrics?.totalVentas.toLocaleString() }}</h2>
										<div class="small text-muted">
											<FontAwesomeIcon :icon="['fas', 'users']" class="me-1" />
											<strong class="fst-italic">{{ kpi1Metrics?.clientes }}</strong> Clientes
										</div>
									</div>

									<div class="col-md-4 px-md-4">
										<span class="x-small fw-bold text-muted text-uppercase d-block mb-1">Semestre pasado</span>
										<div class="fw-bold h5 mb-0 text-secondary">${{ kpi1Metrics?.totalVentas6M.toLocaleString() }}</div>
										<div class="x-small text-muted">
											<FontAwesomeIcon :icon="['fas', 'users']" class="me-1" />
											<strong>{{ kpi1Metrics?.clientes6M }}</strong> Clientes
										</div>
									</div>

									<div class="col-md-3 text-md-end">
										<span class="x-small fw-bold text-muted text-uppercase d-block mb-1">Crecimiento</span>
										<div :class="[(kpi1Metrics?.crecimientoAbsoluto ?? -1) >= 0 ? 'text-success' : 'text-danger']"
											class="h4 fw-bold mb-0">
											{{ (kpi1Metrics?.crecimientoAbsoluto ?? -1) >= 0 ? '+' : '-' }}${{
												Math.abs(kpi1Metrics?.crecimientoAbsoluto ?? 0).toLocaleString() }}
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>

					<div class="col-12">
						<div class="card border-0 shadow-sm table-card bg-white">
							<div class="card-header bg-transparent py-3 border-bottom-light">
								<h6 class="mb-0 fw-bold text-dark x-small text-uppercase tracking-wider">Desglose
									Detallado</h6>
							</div>
							<div class="card-body p-0">
								<div class="table-responsive">
									<table class="table align-middle mb-0">
										<thead>
											<tr class="x-small text-muted text-uppercase">
												<th class="ps-4 py-3 border-0">Categoría</th>
												<th class="border-0">Mes Actual</th>
												<th class="border-0">Semestre pasado (Prom)</th>
												<th class="border-0 text-end">Variación</th>
												<th class="pe-4 border-0 text-end">Var. %</th>
											</tr>
										</thead>
										<tbody>
											<template v-if="hasData">
												<tr v-for="(kpi, index) in kpiMetrics" :key="index">
													<td class="ps-4">
														<div class="d-flex align-items-center">
															<div class="status-dot me-2" :style="{ backgroundColor: kpi.color }"></div>
															<span class="fw-bold text-dark small">{{ kpi.segmento }}</span>
														</div>
													</td>
													<td>
														<div class="fw-bold text-dark small">${{ kpi.totalVentas.toLocaleString() }}</div>
														<div class="x-small fst-italic">{{ kpi.clientes }} clientes</div>
													</td>
													<td>
														<div class="text-secondary small">${{ kpi.totalVentas6M.toLocaleString() }}</div>
														<div class="x-small text-muted-light">{{ kpi.clientes6M }} clientes</div>
													</td>
													<td class="text-end">
														<div :class="[kpi.crecimientoAbsoluto >= 0 ? 'text-success' : 'text-danger', 'fw-bold small']">
															{{ kpi.crecimientoAbsoluto >= 0 ? '+' : '' }}
															${{ kpi.crecimientoAbsoluto.toLocaleString() }}
														</div>
													</td>
													<td class="pe-4 text-end">
														<span :class="[
															kpi.crecimientoPorcentual >= 0 ? 'bg-light-success text-success' : 'bg-light-danger text-danger',
															'badge rounded-pill fw-bold'
														]">
															<FontAwesomeIcon
																:icon="kpi.crecimientoPorcentual >= 0 ? ['fas', 'caret-up'] : ['fas', 'caret-down']"
																class="me-1" />
															{{ Math.abs(kpi.crecimientoPorcentual).toFixed(2) }}%
														</span>
													</td>
												</tr>
											</template>
											<tr v-else>
												<td colspan="5" class="py-5 text-center">
													<div class="text-muted">
														<FontAwesomeIcon :icon="['fas', 'folder-open']" class="fs-3 mb-2 d-block mx-auto opacity-50" />
														<p class="mb-0">No se encontraron registros para este periodo</p>
													</div>
												</td>
											</tr>
										</tbody>
									</table>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<script setup lang="ts">
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { ref, computed, onUnmounted, watch, nextTick } from 'vue'
import Chart from 'chart.js/auto'
import { useDashboardStore } from '@/dashboard/store/dashboardStore'
import type { Kpi1 } from '@/dashboard/types/metrics'

const store = useDashboardStore()
const kpi1Metrics = computed<Kpi1 | null>(() => store.data?.kpi1?.find(n => n.segmento === 'Recurrentes') ?? null)

const hasData = computed(() => kpiMetrics.value && kpiMetrics.value.length > 0)
const kpiMetrics = computed(() => {
	const rawData = store.data?.kpi2 || []

	const colorsPerSegmento: Record<string, string> = {
		'Incremento en Venta': '#6366f1',
		'Venta Estable': '#10b981',
		'Decremento en Venta': '#f59e0b',
		'Venta Perdida': '#ef4444'
	}
	return rawData.map(m => ({
		...m,
		color: colorsPerSegmento[m.segmento]
	}))
})

const chartCanvas = ref<HTMLCanvasElement | null>(null)
let chartInstance: Chart | null = null

watch(() => kpiMetrics.value, async (newVal) => {
	if (!newVal || newVal.length === 0) return

	await nextTick();

	if (chartCanvas.value) {
		if (chartInstance) {
			chartInstance.destroy()
		}

		chartInstance = new Chart(chartCanvas.value, {
			type: 'doughnut',
			data: {
				labels: newVal.map(m => m.segmento),
				datasets: [{
					data: newVal.map(m => m.porcentajeVentas),
					backgroundColor: newVal.map(m => m.color),
					borderWidth: 2,
					borderColor: '#ffffff'
				}]
			},
			options: {
				responsive: true,
				maintainAspectRatio: false,
				cutout: '75%',
				plugins: {
                    legend: { display: false },
                    tooltip: {
                        callbacks: {
                            label: function(context) {
                                const label = context.label || '';
                                const value = context.raw || 0;
                                // Esto hace que muestre: " Incremento en Venta: 50%"
                                return ` ${label}: ${value}%`;
                            }
                        }
                    }
                }
			}
		})
	}
}, { deep: true, immediate: true })

onUnmounted(() => chartInstance?.destroy())
</script>

<style scoped src="./styles.css"></style>
