<template>
	<section class="mb-5">
		<div class="d-flex align-items-center mb-4">
			<div style="width: 4px; height: 24px; background-color: #00a3ff;" class="rounded-pill me-2"></div>
			<h5 class="fw-bold mb-0 text-dark">Resumen de ventas</h5>
		</div>

		<div class="row g-3 row-cols-1 row-cols-md-3 row-cols-xl-5">
			<div class="col" v-for="(kpi, index) in kpiMetrics" :key="index">
				<div
					class="card border-0 shadow-sm kpi-card h-100"
					:class="isVentaTotal(kpi) ? 'card-total-highlight' : ''"
				>
					<div class="card-body">

						<div class="d-flex justify-content-between align-items-start mb-3">
							<h6 class="text-muted small text-uppercase fw-bold mb-0 tracking-wider">{{ kpi.segmento }}</h6>
							<span :class="getStatusBadgeClass(kpi.crecimientoPorcentual)">
								<FontAwesomeIcon :icon="kpi.crecimientoPorcentual >= 0 ? ['fas', 'chevron-up'] : ['fas', 'chevron-down']" />
								{{ Math.abs(kpi.crecimientoPorcentual).toFixed(1) }}%
							</span>
						</div>

						<div class="mb-3">
                            <h3
								class="fw-bold mb-0"
								:class="isVentaTotal(kpi) ? 'total-amount' : 'text-dark'">
								${{ kpi.totalVentas.toLocaleString() }}
							</h3>
                            <div class="text-muted x-small d-flex align-items-center mt-1">
                                <FontAwesomeIcon :icon="['fas', 'users']" class="me-1" />
                                <span>
									<strong>{{ kpi.clientes }}</strong> Clientes
								</span>
                            </div>
                        </div>

						<div class="bg-light rounded-3 p-2 mb-2">
							<div class="d-flex justify-content-between align-items-center mb-1">
								<span class="text-muted x-small fw-semibold">Semestre Pasado</span>
								<span class="fw-bold small text-secondary">${{ kpi.totalVentas6M.toLocaleString() }}</span>
							</div>

							<div class="d-flex justify-content-between align-items-center">
								<div class="d-flex align-items-center">
									<FontAwesomeIcon icon="fa-solid fa-users" class="me-1 text-muted" style="font-size: 0.8rem;" />
									<span class="text-muted x-small">Clientes</span>
								</div>
								<span class="small text-secondary fw-medium">{{ kpi.clientes6M }}</span>
							</div>
						</div>

						<div class="d-flex justify-content-between align-items-center pt-2 border-top border-light">
                            <span class="text-muted x-small">Crecimiento Abs.</span>
                            <span
								:class="[kpi.crecimientoAbsoluto >= 0 ? 'text-success' : 'text-danger', 'growth-abs']"
								class="fw-bold">
                                {{ kpi.crecimientoAbsoluto >= 0 ? '+' : '-' }}${{ Math.abs(kpi.crecimientoAbsoluto).toLocaleString() }}
                            </span>
                        </div>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<script setup lang="ts">
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { useDashboardStore } from '@/dashboard/store/dashboardStore'
import { computed } from 'vue'
import type { Kpi1 } from '@/dashboard/types/metrics'

const store = useDashboardStore()

const kpiMetrics = computed(() => {
	const rawData = store.data?.kpi1 || []
	return [...rawData].sort((a, b) => {
		if (a.segmento === 'Venta Total') return -1
		if (b.segmento === 'Venta Total') return 1
		return 0
	})
})

const getStatusBadgeClass = (value: number) => {
	if (value > 0) return 'badge rounded-pill bg-success-subtle text-success fw-bold'
	if (value < 0) return 'badge rounded-pill bg-danger-subtle text-danger fw-bold'
	return 'badge rounded-pill bg-secondary-subtle text-secondary fw-bold'
}

const isVentaTotal = (kpi: Kpi1) => kpi.segmento === 'Venta Total'
</script>

<style scoped src="./styles.css"></style>