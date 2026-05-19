using static DinamicaComercial.Server.ConfigState;

namespace DinamicaComercial.Server.Middlewares
{
    public class ConfigMiddleware(RequestDelegate request)
    {
        public async Task InvokeAsync(
            HttpContext context,
            ConfigState configState)
        {
            if (context.Request.Headers.TryGetValue("mode", out var modeValues))
            {
                var rawMode = modeValues.FirstOrDefault();
                if (Enum.TryParse<ApplicationMode>(rawMode, ignoreCase: true, out var parsedMode)) configState.Mode = parsedMode;
                else configState.Mode = ApplicationMode.none;
            }

            await request(context);
        }
    }
}
