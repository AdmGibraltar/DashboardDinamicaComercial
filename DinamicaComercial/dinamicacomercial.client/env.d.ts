/// <reference types="vite/client" />
interface ImportMetaEnv {
    readonly VITE_MOCK_USER: string
    readonly VITE_IIS_APP_NAME: string
    readonly VITE_IS_SIANCENTRAL: string
}

interface ImportMeta {
    readonly env: ImportMetaEnv
}