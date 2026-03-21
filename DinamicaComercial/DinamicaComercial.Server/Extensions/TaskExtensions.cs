namespace DinamicaComercial.Server.Extensions
{
    public static class TaskExtensions
    {
        public static async Task<T?> SafeExecute<T>(this Task<T> task, T? defaultValue = default)
        {
            try { return await task; }
            catch
            {
                if (defaultValue is not null) return defaultValue;
                return default;
            }
        }
    }
}