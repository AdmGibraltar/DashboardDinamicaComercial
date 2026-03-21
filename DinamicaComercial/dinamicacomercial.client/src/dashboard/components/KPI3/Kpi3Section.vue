<template>
  <section id="kpi3" class="mb-5">
    <div class="d-flex align-items-center mb-4">
      <div class="custom-indicator me-2"></div>
      <h5 class="fw-bold mb-0 text-dark">Adquisición de Clientes Ganados</h5>
    </div>

    <div class="row g-3">
      <div class="col-xl-4 col-md-6">
        <div class="card border-0 shadow-sm kpi-card-simple">
          <div class="card-body p-4">
            
            <div class="d-flex justify-content-between align-items-start mb-3">
              <h6 class="text-muted small text-uppercase fw-bold mb-0 tracking-wider">Total Clientes Ganados</h6>
              <span class="badge rounded-pill bg-success-subtle text-success fw-bold px-3">
                <i class="bi bi-person-plus-fill me-1"></i> +12%
              </span>
            </div>

            <div class="mb-4">
              <div class="d-flex align-items-baseline gap-2">
                <h2 class="fw-bold mb-0 text-dark">185</h2>
                <span class="text-muted small fw-medium">Nuevos Clientes</span>
              </div>
              <h4 class="fw-bold text-primary mt-1">$450,200 <span class="x-small text-muted fw-normal">Venta inicial</span></h4>
            </div>

            <div class="past-period-box p-3 rounded-3">
              <div class="d-flex justify-content-between align-items-center mb-1">
                <span class="text-muted x-small fw-bold text-uppercase">Semestre Pasado</span>
                <span class="fw-bold text-secondary">165 <span class="x-small fw-normal">cls</span></span>
              </div>
              <div class="d-flex justify-content-between align-items-center">
                <span class="text-muted x-small">Monto Anterior</span>
                <span class="small text-secondary fw-medium">$398,000</span>
              </div>
            </div>

            <div class="d-flex justify-content-between align-items-center mt-3 pt-3 border-top border-light-subtle">
              <span class="text-muted x-small">Incremento de Cartera</span>
              <span class="text-success fw-bold x-small">+20 Clientes</span>
            </div>

          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
.custom-indicator {
  width: 4px;
  height: 24px;
  background-color: #00a3ff;
  border-radius: 50px;
}

.kpi-card-simple {
  border: 1px solid #f1f5f9;
  border-radius: 12px;
  transition: transform 0.2s ease;
}

.kpi-card-simple:hover {
  transform: translateY(-4px);
  border-color: #00a3ff;
}

/* El fondo gris característico de tu dashboard */
.past-period-box {
  background-color: #f8f9fa;
  border: 1px solid #edf2f7;
}

.bg-success-subtle { background-color: #d1e7dd; }
.text-success { color: #198754 !important; }
.text-primary { color: #00a3ff !important; }

.x-small { font-size: 0.72rem; }
.tracking-wider { letter-spacing: 0.5px; }
</style>