import { reactive, ref, type Reactive, type Ref } from 'vue'
import type { ModalOptions } from './types'

interface Modal {
    isVisible: Ref<boolean>
    options: Reactive<ModalOptions>
    open: (opts: ModalOptions) => void
    close: () => void
}

export function useModal(): Modal {
    const isVisible = ref<boolean>(false)
    const options = reactive<ModalOptions>({
        showConfirmButton: true,
        showCloseButton: true,
        closeOnBackdropClick: true,
        showCloseButtonInHeader: true,
        animation: 'fade'
    })

    const open = (opts: ModalOptions) => {
        Object.assign(options, opts)
        isVisible.value = true
        document.body.classList.add('modal-open')
    }

    const close = () => {
        isVisible.value = false
        document.body.classList.remove('modal-open')
        options.onClose?.()
    }

    return {
        isVisible,
        options,
        open,
        close
    }
}