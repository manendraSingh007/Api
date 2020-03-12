

using Api.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Mime;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        { _logger = logger;
            Configuration = configuration;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            _logger.LogInformation("Start services");
            var identityurl = Configuration.GetSection("DoverConfig").GetSection("IdentityURL");
            var Audience = Configuration.GetSection("DoverConfig").GetSection("Audience");
            var Connection = Configuration.GetSection("DoverConfig").GetSection("DbConnection");
            services.Configure<DoverConfig>(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<DoverContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(Connection.Value.ToString())));
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = identityurl != null ? identityurl.Value.ToString() : "";
                    options.RequireHttpsMetadata = false;
                    options.Audience = Audience != null ? Audience.Value.ToString() : ""; ;
                });
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);

                    // TODO: add `using using System.Net.Mime;` to resolve MediaTypeNames
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                    return result;
                };
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
                options.ClientErrorMapping[404].Link =
                    "https://httpstatuses.com/404";
            }); ;
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error/500");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            var logsfile =  Configuration.GetSection("DoverConfig").GetSection("LogsFile");
            loggerFactory.AddFile(logsfile.Value.ToString() + "/DoverAPI-{Date}.txt");
            //app.UseStatusCodePagesWithReExecute("/error/{0}");
            //app.UseExceptionHandler("/error/500");
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                Message<PartPrice> msg = new Message<PartPrice>();
                msg.IsSuccess = false;
                msg.ReturnMessage = "server error";
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;
                msg.Error = JsonConvert.SerializeObject(new { error = exception.Message });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(msg.Error);

            }));
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
            pattern: "{controller}/{action}/{id?}"
                    );
            });

        }
    }
}