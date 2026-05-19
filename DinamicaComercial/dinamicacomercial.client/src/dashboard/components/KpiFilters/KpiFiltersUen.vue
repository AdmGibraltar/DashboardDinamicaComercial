<template>
    <div>
        <label for="uen" class="filter-label" :class="{ 'opacity-50': props.disabled }">UEN</label>
        <select
            v-model="store.filters.uen"
            class="form-select form-select-sm custom-input"
            id="uen"
            :disabled="props.disabled"
        >
            <option :value="null">Todas las UEN</option>
            <option :value="uen.id" v-for="uen in uens" :key="uen.id">
                {{ uen.name }}
            </option>
        </select>
    </div>
</template>

<script setup lang="ts">
import { useDashboardStore } from '@/dashboard/store/dashboardStore'
import type { catalogs } from '@/dashboard/types/catalogs'
import { computed } from 'vue'

const props = defineProps<{
    disabled?: boolean
}>()

const store = useDashboardStore()
const uens = computed(() => store.catalogs.uens as catalogs[])
</script>