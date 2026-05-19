<template>
    <div>
        <label for="segmento" class="filter-label" :class="{ 'opacity-50': props.disabled }">Segmentos</label>
        <select
            v-model="store.filters.segmento"
            class="form-select form-select-sm custom-input"
            id="segmento"
            :disabled="props.disabled"
        >
            <option :value="null">Todos los Segmentos</option>
            <option :value="uen.id" v-for="uen in segmentos" :key="uen.id">
                {{ uen.name }}
            </option>
        </select>
    </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/core/store/authStore'
import { useDashboardStore } from '@/dashboard/store/dashboardStore'
import type { catalogs } from '@/dashboard/types/catalogs'
import { ref, watch } from 'vue'
import { apiService } from '@/core/services/api-service'

const props = defineProps<{
    disabled?: boolean
}>()

const store = useDashboardStore()
const authStore = useAuthStore()
const segmentos = ref<catalogs[]>([])

const getSegmentos = async (uenId: number) => {
    try {
        const sucursalId = authStore.isSIANCENTRALMode
        ? store.filters.sucursal?.[0]?.id
        : authStore.sucursalId

        const url = `segmentos?sucursalId=${sucursalId}&uenId=${uenId}`
        const segmentosResponse = await apiService.fetch(url)

        if (segmentosResponse.ok) segmentos.value = await segmentosResponse.json() as catalogs[]
    } catch {}
}

watch(
    () => store.filters.uen,
    async (newValue) => {
        segmentos.value = []
        store.filters.segmento = null
        if (newValue !== null && newValue !== undefined) {
            getSegmentos(newValue)
        }
    },
    { immediate: true }
)
</script>