<template>
    <nav class="navbar navbar-expand-lg sticky-top shadow-sm custom-header py-2">
        <div class="container-fluid px-md-4">
            <a href="#" class="navbar-brand d-flex align-items-center">
                <img
                    src="../../assets/logo.png"
                    alt="Logo"
                    class="me-2 logo"
                >
                <span class="fw-bold text-white d-none d-sm-inline">Dinámica Comercial</span>
            </a>

            <div class="ms-auto">
                <a :href="backUrl" class="back-link d-flex align-items-center text-decoration-none px-2 py-1 rounded">
                    <FontAwesomeIcon icon="fa-arrow-left" class="me-2 icon-sm" />
                    <span class="d-none d-sm-inline fw-semibold">Regresar a sianweb</span>
                </a>
            </div>
        </div>
    </nav>
</template>

<script setup lang="ts">
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { onMounted, ref } from 'vue'

const backUrl = ref<string>('#')
onMounted(() => {
    if (import.meta.env.MODE === 'production') {
        const referrer = document.referrer
        if (referrer && !referrer.includes(window.location.hostname + `/${import.meta.env.VITE_IIS_APP_NAME}`)) {
            localStorage.setItem('url_retorno', referrer)
            backUrl.value = referrer
        } else {
            const savedUrl = localStorage.getItem('url_retorno')
            backUrl.value = savedUrl || '#'
        }
    }
})
</script>

<style scoped>
.custom-header {
    background-color: #00a3ff;
    border-bottom: 1px solid #0086d4;
    min-height: 70px;
}
.logo {
    width: 80px;
    max-height: 40px;
    height: auto;
    object-fit: contain;
    filter: drop-shadow(0 1px 3px rgba(0, 0, 0, 0.15));
}
.fw-bold {
    color: #ffffff !important;
    letter-spacing: -0.5px;
}
.navbar-brand:hover .fw-bold {
    color: #ffffff !important;
    text-shadow: 0 0 8px rgba(255, 255, 255, 0.5);
    transition: all 0.3s ease;
}
.back-link {
    color: rgba(255, 255, 255, 0.9);
    font-size: 0.9rem;
    transition: all 0.2s ease-in-out;
}
.back-link:hover :deep(svg) {
    filter: drop-shadow(0 0 2px rgba(255, 255, 255, 0.5));
    background-color: rgba(255, 255, 255, 0.15); /* Efecto de botón sutil */
    color: #ffffff;
}
.icon-sm {
    font-size: 0.85rem;
}
</style>