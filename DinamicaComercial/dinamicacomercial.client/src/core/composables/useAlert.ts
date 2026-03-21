import Swal, { type SweetAlertResult } from 'sweetalert2'

interface Alert {
    loading(title?: string, text?: string): void
    close(): void
}

export function useAlert(): Alert {
    const loading = (title: string = 'Cargando...', text: string = ''): void => {
        Swal.fire({
            title,
            text,
            allowOutsideClick: false,
            didOpen: () => {
                Swal.showLoading()
            }
        })
    }

    const close = (): void => Swal.close()

    return {
        loading,
        close
    }
}