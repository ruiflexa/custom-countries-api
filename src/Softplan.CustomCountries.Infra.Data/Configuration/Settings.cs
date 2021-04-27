namespace Softplan.CustomCountries.Infra.Data.Configuration
{
    public class Settings
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string SoftplanUrl { get; set; }
        public Graphcountries GraphCountries { get; set; }
        public Jsondb JsonDb { get; set; }
        public Swaggeroptions SwaggerOptions { get; set; }
        public string Secret { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Graphcountries
    {
        public string BaseUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Jsondb
    {
        public string BaseUrl { get; set; }
    }

    public class Swaggeroptions
    {
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public string UIEndpoint { get; set; }
    }

}
