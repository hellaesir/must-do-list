using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Dto.Configuration
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
