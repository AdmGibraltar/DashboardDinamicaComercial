<template>
	<section id="kpi2" class="mb-5">
		<div class="d-flex align-items-center mb-4">
			<div class="custom-indicator me-2"></div>
			<h5 class="fw-bold mb-0 text-dark">Comportamiento de Recurrencia</h5>
		</div>

		<div class="row g-4">
			<div class="col-12 col-xl-5">
				<div class="card border-0 shadow-sm h-100 kpi-card">
					<div class="card-body d-flex flex-column align-items-center justify-content-center p-4">
						<h6 class="text-muted fw-bold text-uppercase x-small mb-4 tracking-wider">Distribución de
							Cartera</h6>
						<div class="chart-container">
							<canvas id="behaviorChart" ref="chartCanvas"></canvas>
							<div class="chart-center-text">
								<span class="text-muted x-small">Cartera</span>
								<span class="fw-bold text-dark fs-4">100%</span>
							</div>
						</div>
						<div class="d-flex flex-wrap justify-content-center mt-4 gap-3 d-xl-none">
							<div v-for="item in behaviorMetrics" :key="item.title" class="d-flex align-items-center">
								<div class="status-dot me-1" :style="`background-color: ${item.color}`"></div>
								<span class="x-small fw-bold text-muted">{{ item.title }}</span>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="col-12 col-xl-7">
				<div class="row g-3">

					<div class="col-12">
						<div class="card border-0 shadow-sm bg-corporate text-white main-recurrence-slim">
							<div class="card-body py-3 px-4">
								<div class="row align-items-center">
									<div class="col-md-5">
										<span class="x-small fw-bold opacity-75 text-uppercase d-block mb-1">Mes
											Actual</span>
										<h2 class="fw-bold mb-0">$920,000</h2>
										<div class="small opacity-90"><i class="bi bi-people-fill me-1"></i> 1,350 <span
												class="x-small">Clientes</span></div>
									</div>

									<div class="col-md-4">
										<div class="past-period-highlight p-2 px-3 rounded-3">
											<span class="x-small fw-bold opacity-75 text-uppercase d-block">Semestre
												Pasado</span>
											<div class="fw-bold h5 mb-0">$845,500</div>
											<div class="x-small opacity-75">1,210 Clientes</div>
										</div>
									</div>

									<div class="col-md-3 text-end border-start border-white border-opacity-25">
										<span
											class="x-small fw-bold opacity-75 text-uppercase d-block">Crecimiento</span>
										<div class="h4 fw-bold mb-0">+8.2%</div>
										<div class="x-small fw-bold text-white-50">+$74,500</div>
									</div>
								</div>
							</div>
						</div>
					</div>

					<div class="col-12">
						<div class="card border-0 shadow-sm table-card">
							<div class="card-header bg-white py-3 border-bottom-light">
								<h6 class="mb-0 fw-bold text-dark x-small text-uppercase">Desglose Comparativo</h6>
							</div>
							<div class="card-body p-0">
								<div class="table-responsive">
									<table class="table table-hover align-middle mb-0">
										<thead class="bg-light-subtle">
											<tr class="x-small text-muted text-uppercase fw-bold">
												<th class="ps-4 py-3">Categoría</th>
												<th>Mes Actual</th>
												<th>Sem. Pasado</th>
												<th class="text-end">Crec. Absoluto</th>
												<th class="text-end pe-4">Variación</th>
											</tr>
										</thead>
										<tbody>
											<tr v-for="item in behaviorMetrics" :key="item.title">
												<td class="ps-4">
													<div class="d-flex align-items-center">
														<div class="status-dot me-2 shadow-sm"
															:style="`background-color: ${item.color}`"></div>
														<span class="fw-bold text-dark small">{{ item.title }}</span>
													</div>
												</td>
												<td>
													<div class="fw-bold text-dark small">${{
														item.periodoActual.monto.toLocaleString() }}</div>
													<div class="x-small text-muted">{{ item.periodoActual.clientes }}
														cls</div>
												</td>
												<td>
													<div class="text-secondary small">${{
														item.periodoAnterior.monto.toLocaleString() }}</div>
													<div class="x-small text-muted opacity-75">{{
														item.periodoAnterior.clientes }} cls</div>
												</td>
												<td class="text-end">
													<div class="small fw-semibold" :class="getTrendClass(item)">
														{{ item.periodoActual.monto - item.periodoAnterior.monto >= 0 ?
														'+' : '' }}${{ (item.periodoActual.monto -
															item.periodoAnterior.monto).toLocaleString() }}
													</div>
												</td>
												<td class="text-end pe-4">
													<span :class="getTrendClass(item)"
														class="fw-bold small px-2 py-1 rounded-pill bg-light">
														{{ calculateVar(item) }}%
													</span>
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
import { ref, reactive, onMounted } from 'vue'
import Chart from 'chart.js/auto'

const chartCanvas = ref<HTMLCanvasElement | null>(null)

const behaviorMetrics = reactive([
	{
		title: 'Estable',
		percentage: 50,
		color: '#00a3ff',
		periodoAnterior: { monto: 8500, clientes: 60 },
		periodoActual: { monto: 9271, clientes: 65 }
	},
	{
		title: 'Incremento',
		percentage: 20,
		color: '#10b981',
		periodoAnterior: { monto: 900, clientes: 30 },
		periodoActual: { monto: 1200, clientes: 45 }
	},
	{
		title: 'Decremento',
		percentage: 20,
		color: '#f59e0b',
		periodoAnterior: { monto: 650, clientes: 12 },
		periodoActual: { monto: 400, clientes: 8 }
	},
	{
		title: 'Perdidos',
		percentage: 10,
		color: '#ef4444',
		periodoAnterior: { monto: 300, clientes: 15 },
		periodoActual: { monto: 0, clientes: 12 }
	}
])

const calculateVar = (item: any) => {
	if (item.periodoAnterior.monto === 0 && item.periodoActual.monto === 0) return '0.0';
	if (item.periodoAnterior.monto === 0) return '100.0';
	return (((item.periodoActual.monto - item.periodoAnterior.monto) / item.periodoAnterior.monto) * 100).toFixed(1);
}

const getTrendClass = (item: any) => {
	const diff = item.periodoActual.monto - item.periodoAnterior.monto;
	if (item.title === 'Perdidos' || diff < 0) return 'text-danger';
	if (diff > 0) return 'text-success';
	return 'text-muted';
}

onMounted(() => {
	if (chartCanvas.value) {
		const ctx = chartCanvas.value.getContext('2d');
		if (ctx) {
			new Chart(ctx, {
				type: 'doughnut',
				data: {
					labels: behaviorMetrics.map(m => m.title),
					datasets: [{
						data: behaviorMetrics.map(m => m.percentage),
						backgroundColor: behaviorMetrics.map(m => m.color),
						borderWidth: 0,
						hoverOffset: 15
					}]
				},
				options: {
					responsive: true,
					maintainAspectRatio: false,
					plugins: { legend: { display: false } },
					cutout: '82%'
				}
			});
		}
	}
})
</script>

<style scoped>
.bg-corporate {
	background-color: #00a3ff;
}

.custom-indicator {
	width: 4px;
	height: 24px;
	background-color: #00a3ff;
	border-radius: 50px;
}

/* Banner Refinado */
.main-recurrence-slim {
	border-radius: 12px;
	background: linear-gradient(135deg, #00a3ff 0%, #0086d4 100%);
	box-shadow: 0 8px 16px rgba(0, 163, 255, 0.15) !important;
}

.past-period-highlight {
	background-color: rgba(255, 255, 255, 0.12);
	border: 1px solid rgba(255, 255, 255, 0.15);
}

.text-white-50 {
	color: rgba(255, 255, 255, 0.7) !important;
}

/* Gráfica */
.chart-container {
	position: relative;
	width: 100%;
	height: 240px;
}

.chart-center-text {
	position: absolute;
	top: 50%;
	left: 50%;
	transform: translate(-50%, -50%);
	text-align: center;
}

/* Tabla */
.table-card {
	border: 1px solid #f1f5f9;
	border-radius: 12px;
}

.border-bottom-light {
	border-bottom: 1px solid #f1f5f9;
}

.table thead th {
	border: 0;
}

.table tbody tr {
	border-bottom: 1px solid #f8f9fa;
}

.status-dot {
	width: 10px;
	height: 10px;
	border-radius: 50%;
}

/* Utilidades */
.x-small {
	font-size: 0.72rem;
	letter-spacing: 0.5px;
}

.tracking-wider {
	letter-spacing: 1px;
}

canvas {
	filter: drop-shadow(0px 8px 12px rgba(0, 0, 0, 0.06));
}
</style>