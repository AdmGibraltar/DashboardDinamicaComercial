<template>
    <div class="loader-overlay">
        <div class="spinner"></div>
        <p>Cargando aplicación...</p>
    </div>
</template>


<style scoped>
.loader-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background-color: rgba(255, 255, 255, 0.9);
    /* Fondo semi-transparente */
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    z-index: 9999;
    /* Asegura que esté por encima de todo */
}

.spinner {
    width: 50px;
    height: 50px;
    border: 5px solid #f3f3f3;
    border-top: 5px solid #3498db;
    /* Color azul */
    border-radius: 50%;
    animation: spin 1s linear infinite;
    margin-bottom: 15px;
}

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }

    100% {
        transform: rotate(360deg);
    }
}

p {
    font-family: sans-serif;
    color: #555;
    font-size: 1.1rem;
}
</style>