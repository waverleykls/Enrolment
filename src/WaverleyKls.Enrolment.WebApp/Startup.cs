﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using WaverleyKls.Enrolment.EntityModels;
using WaverleyKls.Enrolment.Helpers;
using WaverleyKls.Enrolment.Helpers.Interfaces;
using WaverleyKls.Enrolment.Services;
using WaverleyKls.Enrolment.Services.Interfaces;
using WaverleyKls.Enrolment.Settings;
using WaverleyKls.Enrolment.WebApp.Contexts;
using WaverleyKls.Enrolment.WebApp.Settings;

using WebApiContrib.Core.Formatter.Csv;

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
            var connStrings = Configuration.Get<ConnectionStringsSettings>("connectionStrings");
            services.AddDbContext<WklsDbContext>(o => o.UseSqlServer(connStrings.WklsDbContext));

            services.AddMvc(o =>
                    {
                        o.OutputFormatters.Add(new CsvOutputFormatter(new CsvFormatterOptions() { CsvDelimiter = "," }));
                        o.FormatterMappings.SetMediaTypeMappingForFormat("csv", MediaTypeHeaderValue.Parse("text/csv"));
                    })
                    .AddMvcOptions(o => o.Filters.Add(new RequireHttpsAttribute()))
                    .AddJsonOptions(o =>
                    {
                        o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        o.SerializerSettings.Converters.Add(new StringEnumConverter());
                        o.SerializerSettings.Formatting = Formatting.Indented;
                        o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        o.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                        o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    });

            services.AddAuthentication(o => o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddScoped<IWklsDbContext>(p => p.GetService<WklsDbContext>());

            var sendGrid = Configuration.Get<SendGridSettings>("sendGrid");
            services.AddSingleton<SendGridSettings>(sendGrid);

            var jsonSerialiserSettings = new JsonSerializerSettings()
                                         {
                                             ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                             Converters = { new StringEnumConverter() },
                                             Formatting = Formatting.Indented,
                                             NullValueHandling = NullValueHandling.Ignore,
                                             MissingMemberHandling = MissingMemberHandling.Ignore,
                                             DateTimeZoneHandling = DateTimeZoneHandling.Utc
                                         };
            services.AddSingleton<JsonSerializerSettings>(jsonSerialiserSettings);

            services.AddTransient<ICookieHelper, CookieHelper>();

            services.AddTransient<IStudentDetailsService, StudentDetailsService>();
            services.AddTransient<IGuardianDetailsService, GuardianDetailsService>();
            services.AddTransient<IEmergencyContactDetailsService, EmergencyContactDetailsService>();
            services.AddTransient<IMedicalDetailsService, MedicalDetailsService>();
            services.AddTransient<IGuardianConsentsService, GuardianConsentsService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ISendGridMailService, SendGridMailService>();
            services.AddTransient<IDownloadService, DownloadService>();

            services.AddTransient<IEnrolmentContext, EnrolmentContext>();
            services.AddTransient<IAdminContext, AdminContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            var cookie = new CookiePolicyOptions()
                         {
                             HttpOnly = HttpOnlyPolicy.Always,
                             Secure = CookieSecurePolicy.Always
                         };
            app.UseCookiePolicy(cookie);

            app.UseStaticFiles();

            app.UseCookieAuthentication();

            var auth = Configuration.Get<AuthenticationSettings>("authentication");
            var openIdConnect = new OpenIdConnectOptions
                                {
                                    ClientId = auth.AzureAd.ClientId,
                                    Authority = $"{auth.AzureAd.AadInstance.TrimEnd('/')}/{auth.AzureAd.TenantId}",
                                    CallbackPath = auth.AzureAd.CallbackPath
                                };
            app.UseOpenIdConnectAuthentication(openIdConnect);

            app.UseMvcWithDefaultRoute();
        }
    }
}
