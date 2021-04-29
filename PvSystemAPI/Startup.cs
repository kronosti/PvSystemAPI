using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PvSystemAPI.Models;
using System.IO;

namespace PvSystemAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var xmlDocumentationPatch = Path.Combine(AppContext.BaseDirectory, "PvSystemAPI.xml");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Main", new OpenApiInfo
                {
                    Title = "PvSystemAPI",
                    Version = "v1",
                    Description = "API PvSystem",
                    Contact = new OpenApiContact() { Name = "Desarrollo API", Email = "pvsystem_support@kronosti.com" },
                });
                c.SwaggerDoc("sgLogin", new OpenApiInfo
                {
                    Title = "PvSystemAPI sgLogin",
                    Version = "v1",
                    Description = "API para seguridad de acceso de PvSystem",
                    Contact = new OpenApiContact() { Name = "Desarrollo API", Email = "pvsystem_support@kronosti.com" }
                });
                //c.TagActionsBy(p => p.HttpMethod);
                c.IncludeXmlComments(xmlDocumentationPatch, includeControllerXmlComments: true);
                c.DescribeAllEnumsAsStrings();
            });


            services.AddDbContext<kronosti_cbsContext>(options =>
                   options.UseSqlServer("Server=198.38.83.200;Database=kronosti_cbs;User ID=kronosti_cbs;Password=c3iwvosbfkqlnpaxgmhz"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Main/swagger.json", "PvSystem Main");
                c.SwaggerEndpoint("/swagger/sgLogin/swagger.json", "PvSystem sgLogin");
                c.RoutePrefix = string.Empty;
            });
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
