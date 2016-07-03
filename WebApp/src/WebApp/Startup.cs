using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FlugDemo.Data;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                // JSON, XML, INI

            #region usersecrets
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
                    // dotnet user-secrets list
            }
            #endregion

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            var cnnString = Configuration["ConnectionStrings:DefaultConnection"];

            #region usersectet 2
            var verySecretPassword = Configuration["verySecretPassword"];
            #endregion

        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region DI
            services.AddTransient<IFlightRepository, FlightEfRepository>();
            #endregion

            #region swagger
            services.AddSwaggerGen();
            #endregion

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                // Id --> id, From --> from
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            }).AddMvcOptions(options => {
                options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            #region JWT
            
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                Authority = "https://steyer-identity-server.azurewebsites.net/identity",
                Audience = "https://steyer-identity-server.azurewebsites.net/identity/resources"
            });
            
            #endregion

            

            app.UseStaticFiles();

            #region nochmal-swagger
            app.UseSwaggerGen();
            app.UseSwaggerUi();
            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
