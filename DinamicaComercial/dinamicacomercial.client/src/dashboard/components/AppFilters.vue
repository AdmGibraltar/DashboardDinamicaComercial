<template>
    <div class="filter-bar shadow-sm sticky-top">
        <div class="container-fluid px-5">
            <div class="row g-3 align-items-end">
                <div class="col-6 col-md-2">
                    <label for="riks" class="filter-label">Riks</label>
                    <select class="form-select form-select-sm custom-input" name="" id="riks">
                        <option>56 - David Bustamante</option>
                    </select>
                </div>

                <div class="col-6 col-md-2">
                    <label for="periodo" class="filter-label">Periodo</label>
                    <VueDatePicker
                        v-model="store.filters.date"
                        :formats="{
                            input: 'MMMM yyyy',
                            preview: '---'
                        }"
                        month-picker
                        auto-apply
                        :locale="es"
                    />
                </div>

                <div class="col-6 col-md-2">
                    <label for="uen" class="filter-label">UEN</label>
                    <select class="form-select form-select-sm custom-input" name="" id="uen">
                        <option selected>Todas las UEN</option>
                    </select>
                </div>

                <div class="col-6 col-md-2">
                    <label for="segmento" class="filter-label">Segmento</label>
                    <select class="form-select form-select-sm custom-input" name="" id="segmento">
                        <option selected>Todos los Segmentos</option>
                    </select>
                </div>

                <div class="col-12 col-md-auto ms-md-auto d-flex align-items-end">
                    <button @click="store.fetchData()" class="btn btn-primary btn-sm px-4 rounded-pill fw-bold shadow-sm w-100">
                        <FontAwesomeIcon icon="fa-filter" class="me-2" />
                        Aplicar Filtros
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { es } from 'date-fns/locale'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { useDashboardStore } from '../store/dashboardStore'

const store = useDashboardStore()
</script>

<style scoped>
.filter-bar {
    background-color: #ffffff;
    border-bottom: 1px solid #e2e8f0;
    padding: 16px 0;
}
.filter-label {
    font-size: 0.7rem;
    font-weight: 700;
    text-transform: uppercase;
    color: #64748b;
    margin-bottom: 6px;
    display: block;
    letter-spacing: 0.5px;
}
.custom-input {
    background-color: #f1f5f9;
    border: 1px solid #e2e8f0;
    color: #1e293b;
    font-weight: 500;
    height: 35px;
    transition: all 0.2s ease;
}
.custom-input:focus {
    background-color: #ffffff;
    border-color: #00a3ff;
    box-shadow: 0 0 0 3px rgba(0, 163, 255, 0.1);
}

.btn-primary {
    background-color: #00a3ff;
    border-color: #00a3ff;
    height: 35px;
}

.btn-primary:hover {
    background-color: #0086d4;
    border-color: #0086d4;
    transform: translateY(-1px);
}

:deep(.dp__input) {
    text-transform: capitalize;
}

:deep(.dp__overlay_cell),
:deep(.dp__overlay_cell_active),
:deep(.dp__overlay_cell_pad) {
    text-transform: capitalize !important;
}
</style>