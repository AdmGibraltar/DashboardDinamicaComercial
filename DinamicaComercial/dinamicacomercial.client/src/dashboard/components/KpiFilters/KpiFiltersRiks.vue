<template>
    <div>
        <label for="riks" class="filter-label" :class="{ 'opacity-50': props.disabled }">Riks</label>
        <select
            v-model="store.filters.rik"
            class="form-select form-select-sm custom-input"
            id="riks"
            :disabled="props.disabled"
        >
            <option :value="null">Todos los Riks</option>
            <option :value="rik.id" v-for="rik in riks" :key="rik.id">
                {{ rik.id }} - {{ rik.name }}
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
const riks = computed(() => store.catalogs.riks as catalogs[])
</script>