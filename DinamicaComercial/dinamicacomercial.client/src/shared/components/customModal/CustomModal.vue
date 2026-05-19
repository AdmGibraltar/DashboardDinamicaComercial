<template>
    <Teleport to="body">
        <div
            v-if="isVisible"
            class="modal fade show d-block"
            tabindex="-1"
            @clic.self="handleBackdropClick"
        >
            <div class="modal-dialog animate__animated" :class="[sizeClass, animationClass]">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">{{ options.title }}</h5>
                        <button
                            type="button"
                            @click="close"
                            class="btn-close"
                            v-if="options.showCloseButtonInHeader"
                        ></button>
                    </div>
                    <div class="modal-body">
                        <slot></slot>
                    </div>
                    <div class="modal-footer" :class="{'justify-content-center': options.centerButtons }">
                        <button
                            type="button"
                            class="btn btn-secondary"
                            @click="close"
                            v-if="options.showCloseButton"
                        >
                            {{ options.closeButtonText || 'Cerrar' }}
                        </button>
                        <button
                            v-if="options.showConfirmButton"
                            type="button"
                            class="btn btn-primary"
                            @click="confirm"
                        >
                            {{ options.confirmButtonText || 'Confirmar' }}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </Teleport>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useModal } from './useModal'

const {
    isVisible,
    options,
    open,
    close
} = useModal()

const sizeClass = computed(() => {
    return options.size ? `modal-${options.size}` : ''
})

const animationClass = computed(() => {
    let animation = ''
    switch (options.animation) {
        case 'fade':
            animation = isVisible ? 'fadeIn' : 'fadeOut'
            break
        case 'flip':
            animation = isVisible ? 'flipInY' : 'flipOutY'
            break
        case 'slide':
            animation = isVisible ? 'slideInDown' : 'slideOutUp'
            break
        case 'bounce':
        default:
            animation = isVisible ? 'bounceIn' : 'bounceOut'
            break
    }
    return `animate__${animation}`
})

const confirm = () => {
    options.onConfirm?.()
    //No cerrar automaticamente, dejar que el callback decida
    //close()
}

const handleBackdropClick = () => {
    if (options.closeOnBackdropClick) close()
}

defineExpose({
    isVisible,
    options,
    open,
    close
})
</script>
<style scoped src="./styles.css"></style>