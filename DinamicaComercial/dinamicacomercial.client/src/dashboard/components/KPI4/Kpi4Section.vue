<template>
  <section id="kpi4" class="mb-5">
    <div class="d-flex align-items-center mb-4">
      <div class="custom-indicator me-2"></div>
      <h5 class="fw-bold mb-0 text-dark">Motivos de Decremento en Venta</h5>
    </div>

    <div class="row g-4">
      <div class="col-12 col-xl-4">
        <div class="card border-0 shadow-sm h-100">
          <div class="card-body d-flex flex-column align-items-center justify-content-center p-4">
            <h6 class="text-muted fw-bold text-uppercase x-small mb-4">Distribución por Motivo</h6>
            <div class="chart-container-pie">
              <canvas id="lossChart" ref="chartCanvas"></canvas>
            </div>
          </div>
        </div>
      </div>

      <div class="col-12 col-xl-8">
        <div class="card border-0 shadow-sm h-100">
          <div class="card-body p-0">
            <div class="table-responsive">
              <table class="table align-middle mb-0">
                <thead class="bg-light-subtle">
                  <tr class="x-small text-muted text-uppercase fw-bold">
                    <th class="ps-4 py-3">Motivo / Causa</th>
                    <th class="text-center">Prom. Sem. Pasado</th>
                    <th class="text-center">Prom. Actual</th>
                    <th class="text-center">Diferencia</th>
                    <th class="text-end pe-4">Variación</th>
                  </tr>
                </thead>
                <tbody v-for="motivo in lossData" :key="motivo.nombre">
                  <tr class="bg-light bg-opacity-50">
                    <td class="ps-4" colspan="5">
                      <div class="d-flex align-items-center">
                        <div class="status-dot-large me-2" :style="`background-color: ${motivo.color}`"></div>
                        <span class="fw-bold text-dark">{{ motivo.nombre }}</span>
                      </div>
                    </td>
                  </tr>
                  <tr v-for="causa in motivo.causas" :key="causa.descripcion" class="causa-row">
                    <td class="ps-5 text-muted small italic">- {{ causa.descripcion }}</td>
                    <td class="text-center small">${{ causa.promedioPasado.toLocaleString() }}</td>
                    <td class="text-center fw-bold small text-dark">${{ causa.promedioActual.toLocaleString() }}</td>
                    <td class="text-center small" :class="getTrendClass(causa.promedioActual, causa.promedioPasado)">
                      {{ (causa.promedioActual - causa.promedioPasado).toLocaleString() }}
                    </td>
                    <td class="text-end pe-4">
                      <span class="badge rounded-pill bg-light text-dark x-small fw-bold border">
                        {{ calculateVar(causa.promedioActual, causa.promedioPasado) }}%
                      </span>
                    </td>
                  </tr>
                </tbody>
                <tfoot class="bg-dark text-white">
                  <tr>
                    <td class="ps-4 fw-bold">TOTAL GENERAL</td>
                    <td class="text-center fw-bold">${{ totalPasado.toLocaleString() }}</td>
                    <td class="text-center fw-bold">${{ totalActual.toLocaleString() }}</td>
                    <td class="text-center fw-bold text-warning">${{ (totalActual - totalPasado).toLocaleString() }}</td>
                    <td class="text-end pe-4 fw-bold">{{ calculateVar(totalActual, totalPasado) }}%</td>
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
import { ref, computed, onMounted } from 'vue'
import Chart from 'chart.js/auto'

const chartCanvas = ref(null)

const lossData = [
  {
    nombre: 'Key', color: '#00a3ff',
    causas: [
      { descripcion: 'Precio competencia', promedioPasado: 50000, promedioActual: 42000 },
      { descripcion: 'Falta de stock', promedioPasado: 30000, promedioActual: 25000 }
    ]
  },
  {
    nombre: 'Cliente', color: '#f59e0b',
    causas: [
      { descripcion: 'Cierre de sucursal', promedioPasado: 45000, promedioActual: 10000 },
      { descripcion: 'Cambio de política', promedioPasado: 20000, promedioActual: 18000 }
    ]
  },
  {
    nombre: 'no.Perdida', color: '#ef4444',
    causas: [
      { descripcion: 'Error administrativo', promedioPasado: 15000, promedioActual: 5000 }
    ]
  }
]

// Cálculos de Totales
const totalPasado = computed(() => lossData.reduce((acc, m) => acc + m.causas.reduce((a, c) => a + c.promedioPasado, 0), 0))
const totalActual = computed(() => lossData.reduce((acc, m) => acc + m.causas.reduce((a, c) => a + c.promedioActual, 0), 0))

const calculateVar = (act, pas) => pas === 0 ? '0' : (((act - pas) / pas) * 100).toFixed(1)
const getTrendClass = (act, pas) => act < pas ? 'text-danger fw-bold' : 'text-success fw-bold'

onMounted(() => {
  const ctx = chartCanvas.value.getContext('2d')
  new Chart(ctx, {
    type: 'pie',
    data: {
      labels: lossData.map(m => m.nombre),
      datasets: [{
        data: lossData.map(m => m.causas.reduce((a, c) => a + c.promedioActual, 0)),
        backgroundColor: lossData.map(m => m.color),
        borderWidth: 2,
        borderColor: '#ffffff'
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: { legend: { position: 'bottom', labels: { boxWidth: 12, font: { size: 11 } } } }
    }
  })
})
</script>

<style scoped>
.custom-indicator { width: 4px; height: 24px; background-color: #00a3ff; border-radius: 50px; }
.chart-container-pie { position: relative; width: 100%; height: 280px; }
.status-dot-large { width: 12px; height: 12px; border-radius: 50%; }

.causa-row:hover { background-color: #fcfcfc; }
.italic { font-style: italic; }

tfoot { border-top: 2px solid #333; }
.x-small { font-size: 0.72rem; }

/* Estilo para que la tabla no se vea pesada */
.table td { border-bottom: 1px solid #f1f5f9; padding: 12px 8px; }
</style>