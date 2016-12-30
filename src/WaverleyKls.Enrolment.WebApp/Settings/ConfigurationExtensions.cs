using Microsoft.Extensions.Configuration;

namespace WaverleyKls.Enrolment.WebApp.Settings
{
    /// <summary>
    /// This represents the extension entity for <see cref="IConfiguration"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets the strongly-typed instance bound with the given type.
        /// </summary>
        /// <typeparam name="T">Type for binding.</typeparam>
        /// <param name="config"><see cref="IConfiguration"/> instance.</param>
        /// <param name="key">Key value to look for.</param>
        /// <returns>Returns the bound instance.</returns>
        public static T Get<T>(this IConfiguration config, string key) where T : new()
        {
            var instance = new T();
            config.GetSection(key).Bind(instance);

            return instance;
        }
    }
}
