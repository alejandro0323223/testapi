using System;
using log4net.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace rest_remate_linea_admin
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            AddSwagger(services);


            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:4201", "http://integracionplg.plataformagroup.cl", "http://10.170.10.203")
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
            //services.AddAuthentication(IISDefaults.AuthenticationScheme);

            //var connection = Configuration.GetConnectionString("DefaultConnection");

            //services.AddDbContext<RemateEnLinea>(options => options
            //    .UseLazyLoadingProxies()
            //    .UseSqlServer(connection, builder => builder.UseRowNumberForPaging())); 

            //services.AddTransient(typeof(IHelper<>), typeof(Helper<>));
            //services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            //services.AddTransient<IUsuarioComponent, UsuarioComponent>();
        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = "ASP Net Core Rest TEST para principal",
                    Version = groupName,
                    Description = "Rest Api test principal ",
                    Contact = new OpenApiContact
                    {
                        Name = "Alejandro Vejar",
                        Email = "avejarcor@gmail.com",
                        Url = new Uri("https://www.principal.cl/"),
                    }
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
         

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (env.IsDevelopment())
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Principal API test  Swagger");
                }
                else
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Principal API test  Swagger");
                }
               
            });


            app.UseHttpsRedirection();
            app.UseCors("CorsApi");
            app.UseRouting();
        

            app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
