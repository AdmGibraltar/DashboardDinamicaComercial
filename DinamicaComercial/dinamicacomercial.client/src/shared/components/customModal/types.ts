export interface ModalOptions {
    title?: string
    size?: 'sm' | 'md' | 'lg' | 'xl' | 'fullscreen',
    closeOnBackdropClick?: boolean
    animation?: 'bounce' | 'fade' | 'slide' | 'flip'
    showConfirmButton?: boolean
    confirmButtonText?: string
    showCloseButton?: boolean
    closeButtonText?: string
    centerButtons?: boolean
    showCloseButtonInHeader?: boolean
    onConfirm?: () => void
    onClose?: () => void
}