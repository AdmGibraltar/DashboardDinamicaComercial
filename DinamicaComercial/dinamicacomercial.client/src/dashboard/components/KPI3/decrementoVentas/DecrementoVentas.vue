<template>
    <button v-if="showButton" @click.prevent="openModal"
        class="btn btn-primary btn-sm rounded-pill px-3 shadow-sm fw-bold">
        <FontAwesomeIcon :icon="['fas', 'plus']" class="me-1" />
        Registrar Motivo
    </button>

    <CustomModal ref="modalRef">
        <div class="px-2">
            <div class="row g-3 mb-4 align-items-end">
                <div v-if="authStore.isGerente" class="col-md-4">
                    <label class="x-small fw-bold text-muted text-uppercase mb-2 d-block">Riks</label>
                    <select v-model="filterRik" class="form-select shadow-none input-custom-clean">
                        <option :value="null">Todos los Riks</option>
                        <option v-for="r in riks" :key="r.id" :value="r.id">{{ r.name }}</option>
                    </select>
                </div>
                <div :class="authStore.isGerente ? 'col-md-5' : 'col-md-9'">
                    <label class="x-small fw-bold text-muted text-uppercase mb-2 d-block">Buscar por nombre o ID de cliente</label>
                    <div class="input-group">
                        <span class="input-group-text bg-light border-end-0 text-muted">
                            <FontAwesomeIcon :icon="['fas', 'search']" />
                        </span>
                        <input
                            v-model="searchQuery"
                            type="text"
                            class="form-control shadow-none input-custom-clean border-start-0">
                    </div>
                </div>
                <div class="col-md-auto ms-auto">
                    <button
                        @click="showCompleted = !showCompleted"
                        class="btn btn-sm rounded-pill px-3 fw-bold transition-all"
                        :class="showCompleted ? 'btn-outline-primary' : 'btn-light text-muted'"
                    >
                        <FontAwesomeIcon :icon="['fas', showCompleted ? 'eye' : 'eye-slash']" class="me-2" />
                        {{ showCompleted ? 'Ocultar completados' : 'Mostrar completados' }}
                    </button>
                </div>
            </div>

            <div class="modal-table-card shadow-sm bg-white">
                <div class="table-responsive" style="max-height: 500px;">
                    <table class="table align-middle mb-0 table-sm-custom">
                        <thead>
                            <tr class="x-small text-muted text-uppercase border-bottom-light">
                                <th class="ps-4 py-3 border-0" style="width: 50px;">
                                    <input
                                        type="checkbox"
                                        class="form-check-input shadow-none"
                                        @change="toggleSelectAll"
                                        :checked="isAllSelected"
                                    >
                                </th>
                                <th class="border-0">Cliente</th>
                                <th class="border-0 text-end">Venta Actual</th>
                                <th class="border-0 text-end">Venta promedio semestre pasado</th>
                                <th class="border-0 text-center">% Decremento</th>
                                <th class="border-0" style="width: 220px;">Motivo</th>
                                <th class="pe-4 border-0" style="width: 220px;">Causa</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-if="selectedIds.length > 0" class="bg-light-indigo-custom animate__animated animate__fadeIn">
                                <td class="ps-4 text-center">
                                    <FontAwesomeIcon :icon="['fas', 'bolt']" class="text-indigo" />
                                </td>
                                <td colspan="4">
                                    <span class="fw-bold text-indigo small">Aplicar a {{ selectedIds.length }} seleccionados:</span>
                                </td>
                                <td>
                                    <select
                                        @change="applyBulkMotivo"
                                        class="form-select form-select-sm border-indigo shadow-none fw-bold "
                                    >
                                        <option value="">Copiar motivo...</option>
                                        <option v-for="m in motivos" :key="m.id" :value="m.id">{{ m.name }}</option>
                                    </select>
                                </td>
                                <td class="pe-4">
                                    <select
                                        @change="applyBulkCausa"
                                        class="form-select form-select-sm border-indigo shadow-none fw-bold"
                                        :disabled="!bulkMotivoSelected"
                                    >
                                        <option value="">Copiar causa...</option>
                                        <option
                                            v-for="c in getCausasByMotivo(bulkMotivoSelected)"
                                            :key="c.id"
                                            :value="c.id"
                                        >{{ c.name }}</option>
                                    </select>
                                </td>
                            </tr>

                            <tr
                                v-for="item in filteredList"
                                :key="item.id"
                                :class="{ 'bg-selected': selectedIds.includes(item.id) }"
                            >
                                <td class="ps-4">
                                    <div v-if="!item.completado">
                                        <input
                                            type="checkbox"
                                            class="form-check-input shadow-none"
                                            v-model="selectedIds"
                                            :value="item.id"
                                        >
                                    </div>
                                    <div v-else class="d-flex justify-content-center align-items-center" title="Registro completado">
                                        <FontAwesomeIcon :icon="['fas', 'check-circle']" class="text-success fs-5" />
                                    </div>
                                </td>
                                <td>
                                    <div class="fw-bold text-dark small">{{ item.cliente }}</div>
                                    <div class="x-small text-muted">ID: {{ item.idCliente }}</div>
                                </td>
                                <td class="text-end fw-bold text-dark small">
                                    ${{ item.ventaMes.toLocaleString() }}
                                </td>
                                <td class="text-end fw-bold text-dark small">
                                    ${{ item.promedioVenta6M.toLocaleString() }}
                                </td>
                                <td class="text-center">
                                    <span class="badge rounded-pill badge-decrement fw-bold">
                                        <FontAwesomeIcon :icon="['fas', 'caret-down']" class="me-1" />
                                        {{ item.porcentajeDecremento.toLocaleString() }}%
                                    </span>
                                </td>
                                <td>
                                    <select
                                        v-model="item.idMotivo"
                                        :disabled="item.completado"
                                        @change="handleMotivoChange(item)"
                                        class="form-select form-select-sm input-custom-clean"
                                        :class="{ 'input-filled': item.idMotivo }"
                                    >
                                        <option :value="null">-- Seleccione --</option>
                                        <option v-for="m in motivos" :key="m.id" :value="m.id">{{ m.name }}</option>
                                    </select>
                                </td>
                                <td class="pe-4">
                                    <select
                                        v-model="item.idCausa"
                                        :disabled="item.completado"
                                        class="form-select form-select-sm input-custom-clean"
                                        :class="{ 'input-filled': item.idCausa }"
                                    >
                                        <option :value="null">-- Seleccione --</option>
                                        <option
                                            v-for="c in getCausasByMotivo(item.idMotivo)"
                                            :key="c.id"
                                            :value="c.id"
                                        >{{ c.name }}</option>
                                    </select>
                                </td>
                            </tr>

                            <tr v-if="filteredList.length === 0">
                                <td colspan="7" class="py-5 text-center text-muted">
                                    <FontAwesomeIcon :icon="['fas', 'filter']" class="fs-4 mb-2 opacity-50" />
                                    <p class="mb-0 small text-uppercase fw-bold">No hay clientes con decremento para mostrar</p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </CustomModal>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import CustomModal from '@/shared/components/customModal/CustomModal.vue'
import { useDashboardStore } from '@/dashboard/store/dashboardStore'
import { useAuthStore } from '@/core/store/authStore'
import type { catalogs } from '@/dashboard/types/catalogs'
import { useDashboard } from '@/dashboard/composables/useDashboard'
import type { DecrementoVentas } from '@/dashboard/types/decrementoVentas'
import Swal from 'sweetalert2'
import { apiService } from '@/core/services/api-service'

const store = useDashboardStore()
const authStore = useAuthStore()
const { getDecrementoVentas, getDashOnlyKpi3 } = useDashboard()

const modalRef = ref<InstanceType<typeof CustomModal> | null>(null)
const searchQuery = ref<string>('')
const selectedIds = ref<number[]>([])
const filterRik = ref<number | null>(null)
const bulkMotivoSelected = ref<number | null>(null)
const showCompleted = ref<boolean>(false)

const showButton = computed(() => (store.data?.isClosingMonth || false) && !authStore.isSIANCENTRALMode)

const motivos = computed(() => store.catalogs.motivosDecremento)
const list = computed(() => store.decrementoVentas)
const riks = computed(() => store.catalogs.riks as catalogs[])

const filteredList = computed(() => {
    return list.value.filter(c => {
        const matchesSearch = c.cliente?.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
            c.idCliente.toString().includes(searchQuery.value)

        const isCompleted = c.completado

        if (showCompleted.value) {
            return matchesSearch
        } else {
            // Si el switch está OFF, solo mostramos los que NO estén completados
            return matchesSearch && !isCompleted
        }
    })
})

const isAllSelected = computed(() => {
    const selectables = filteredList.value.filter(c => !c.completado)

    if (selectables.length === 0) return false

    return selectables.length === selectedIds.value.length
})

const openModal = async () => {
    if (list.value.length === 0 && store.data?.isClosingMonth) {
        const rikId = authStore.isGerente ? undefined : store.filters.rik ?? undefined
        await getDecrementoVentas(rikId)
    }
    modalRef.value?.open({
        title: 'Motivo de Decremento en Ventas',
        size: 'xl',
        onClose: () => {
            selectedIds.value = []
            searchQuery.value = ''
            bulkMotivoSelected.value = null
            list.value.forEach(item => {
                item.idMotivo = null
                item.idCausa = null
            })
        },
        onConfirm: async () => {
            const itemsSeleccionados = list.value.filter(item =>
                selectedIds.value.includes(item.id)
            )

            const payload = itemsSeleccionados
                .filter(item => item.idMotivo !== null && item.idCausa !== null)
                .map(item => ({
                    idDecrementoVenta: item.id,
                    idMotivo: item.idMotivo,
                    idCausa: item.idCausa
                }))

            if (payload.length === 0) return

            try {
                const response = await apiService.fetch(`decremento-ventas`, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        motivos: payload
                    })
                })

                if (response.ok) {
                    await Swal.fire({
                        icon: 'success',
                        title: '¡Guardado!',
                        text: 'Los motivos se registraron correctamente.',
                        timer: 2000,
                        showConfirmButton: false
                    })

                    selectedIds.value = []
                    modalRef.value?.close()
                    const rikId = authStore.isGerente ? undefined : store.filters.rik ?? undefined
                    await getDecrementoVentas(rikId)
                    await getDashOnlyKpi3()
                }
            } catch {
                Swal.fire({
                    icon: 'error',
                    title: 'Error de conexión',
                    text: 'No se pudieron guardar los cambios. Intenta de nuevo.'
                })
            }
        }
    })
}

const toggleSelectAll = () => {
    if (isAllSelected.value) {
        filteredList.value.forEach(item => {
            if (selectedIds.value.includes(item.id)) {
                item.idMotivo = null
                item.idCausa = null
            }
        })
        selectedIds.value = []
    } else {
        selectedIds.value = filteredList.value
            .filter(c => !c.completado)
            .map(c => c.id)
    }
}

const applyBulkMotivo = (event: Event) => {
    const val = (event.target as HTMLSelectElement).value
    if (!val) {
        bulkMotivoSelected.value = null
        return
    }

    const id = parseInt(val)
    bulkMotivoSelected.value = id

    list.value.forEach(c => {
        if (selectedIds.value.includes(c.id)) {
            c.idMotivo = id
            c.idCausa = null
        }
    })
}

const applyBulkCausa = (event: Event) => {
    const val = (event.target as HTMLSelectElement).value
    if (!val) return

    list.value.forEach(c => {
        if (selectedIds.value.includes(c.id)) c.idCausa = parseInt(val)
    })

    setTimeout(() => (event.target as HTMLSelectElement).value = '', 500)
}

const getCausasByMotivo = (idMotivo: number | null) => {
    if (!idMotivo) return []
    return store.catalogs.causasDecremento.filter(c => c.idMotivo === idMotivo)
}

const handleMotivoChange = (item: DecrementoVentas) => {
    item.idCausa = null
    if (item.idMotivo && !selectedIds.value.includes(item.id)) {
        selectedIds.value.push(item.id)
    }
}

watch(filterRik, async (newVal) => {
    await getDecrementoVentas(newVal ?? undefined)
    selectedIds.value = []
    searchQuery.value = ''
    bulkMotivoSelected.value = null
})

watch(() => list.value, (newList) => {
    newList.forEach(item => {
        if (item.idMotivo) {
            const causasDisponibles = getCausasByMotivo(item.idMotivo)
            const causaActualValida = causasDisponibles.some(c => c.id === item.idCausa)
            if (!causaActualValida) {
                item.idCausa = null
            }
        } else {
            item.idCausa = null
        }
    })
}, { deep: true })
</script>

<style scoped src="./styles.css"></style>
