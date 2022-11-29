using Microsoft.Extensions.Configuration;

namespace MustDoList.Config.Configuration
{
    public class AppSettings
    {
        public Authentication Authentication { get; set; }

        public static AppSettings loadAppSettings(IConfiguration configuration)
        {
            var settings = new AppSettings();
            ConfigurationBinder.Bind(configuration, "AppConfiguration", settings);
            return settings;
        }
    }

    public class Authentication
    {
        public string PrivateToken { get; set; }
    }
}
