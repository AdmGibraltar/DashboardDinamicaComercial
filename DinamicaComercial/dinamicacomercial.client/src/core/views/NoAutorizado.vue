<template>
    <div class="no-access-container">
        <div class="no-access-card">
            <h1>Acceso Denegado</h1>
            <p>No tienes los permisos necesarios para ver esta página.</p>
            <p>Si crees que esto es un error, por favor contacta al administrador del sistema.</p>
        </div>
    </div>
</template>

<style scoped>
.no-access-container {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 80vh;
    text-align: center;
}
.no-access-card {
    padding: 2rem 4rem;
    border-radius: 8px;
    background-color: #f8f9fa;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}
</style>