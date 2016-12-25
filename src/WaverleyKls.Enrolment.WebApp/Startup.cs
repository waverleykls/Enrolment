using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using WaverleyKls.Enrolment.EntityModels;
using WaverleyKls.Enrolment.Helpers;
using WaverleyKls.Enrolment.Services;
using WaverleyKls.Enrolment.Services.Interfaces;
using WaverleyKls.Enrolment.WebApp.Settings;

namespace WaverleyKls.Enrolment.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connStrings = Configuration.Get<ConnectionStringsSettings>("ConnectionStrings");
            services.AddDbContext<WklsDbContext>(o => o.UseSqlServer(connStrings.WklsDbContext));

            // Add framework services.
            services.AddMvc()
                    .AddMvcOptions(o => o.Filters.Add(new RequireHttpsAttribute()))
                    .AddJsonOptions(o =>
                    {
                        o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        o.SerializerSettings.Converters.Add(new StringEnumConverter());
                        o.SerializerSettings.Formatting = Formatting.Indented;
                        o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        o.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    });

            services.AddScoped<IWklsDbContext>(p => p.GetService<WklsDbContext>());

            services.AddTransient<ICookieHelper, CookieHelper>();

            services.AddTransient<IStudentDetailsService, StudentDetailsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var cookie = new CookiePolicyOptions()
                         {
                             HttpOnly = HttpOnlyPolicy.Always,
                             Secure = CookieSecurePolicy.Always
                         };
            app.UseCookiePolicy(cookie);

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
