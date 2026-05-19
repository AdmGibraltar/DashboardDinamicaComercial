namespace DinamicaComercial.Server
{
    public class ConfigState
    {
        public enum ApplicationMode
        {
            none,
            sianweb,
            siancentral
        }
        public ApplicationMode Mode { get; set; } = ApplicationMode.none;
    }
}
