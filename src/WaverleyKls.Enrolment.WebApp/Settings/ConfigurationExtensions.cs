using Microsoft.Extensions.Configuration;

namespace WaverleyKls.Enrolment.WebApp.Settings
{
    public static class ConfigurationExtensions
    {
        public static T Get<T>(this IConfiguration config, string key) where T : new()
        {
            var instance = new T();
            config.GetSection(key).Bind(instance);

            return instance;
        }
    }
}
