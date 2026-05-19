import { onMounted, onUnmounted, ref, type Ref } from 'vue'

export function useScrollToTop(): {
    showScrollTop: Ref<boolean>
    scrollToTop: () => void
} {
    const showScrollTop = ref<boolean>(false)
    
    const scrollToTop = (): void => {
        window.scrollTo({ 
            top: 0,
            behavior: 'smooth'
        })
    }

    const handleScroll = (): void => {
        showScrollTop.value = window.scrollY > 300
    }

    onMounted(() => window.addEventListener('scroll', handleScroll))
    onUnmounted(() => window.removeEventListener('scroll', handleScroll))

    return {
        showScrollTop,
        scrollToTop
    }
}