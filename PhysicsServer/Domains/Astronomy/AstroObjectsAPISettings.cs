namespace PhysicsServer.Domains.Astronomy
{
    public interface IAstroObjectsAPISettings
    {
        public string BaseAddress { get; set; }
    }

    public class AstroObjectsAPISettings : IAstroObjectsAPISettings
    {
        public string BaseAddress { get; set; } = string.Empty;
    }
}
