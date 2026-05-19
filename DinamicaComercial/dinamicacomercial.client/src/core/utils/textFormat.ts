const formatMonth = (date: Date) => {
    const name = date.toLocaleString('es-ES', { month: 'long' })
    return name.charAt(0).toUpperCase() + name.slice(1)
}

export { formatMonth }