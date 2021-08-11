using System;
using BusinessRemateLinea.IBusiness;
using EntityRepo.Entities.IRepository;
using EntityRepo.Entities.Repository;
using EntityRepo.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace rest_remate_linea
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();
            AddSwagger(services);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            //var connection = Configuration.GetConnectionString("DefaultConnection");

            //services.AddDbContext<RemateEnLinea>(options => options
            //    .UseLazyLoadingProxies()
            //    .UseSqlServer(connection));

            //services.AddTransient(typeof(IHelper<>), typeof(Helper<>));
            //services.AddTransient<IrlLoteoAdjudicacionRepository, rlLoteoAdjudicacionRepository>();
            //services.AddTransient<IAdjudicacionComponent, AdjudicacionComponent>();
        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = "ASP Net Core Rest Remates Core",
                    Version = groupName,
                    Description = "Rest Api de Remante en Linea y Core",
                    Contact = new OpenApiContact
                    {
                        Name = "PlataformaGroup",
                        Email = "agutierrez@plataformagroup.cl",
                        Url = new Uri("https://www.plataformagrup.cl/"),
                    }
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsApi");
        

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlataformaGroup Swagger");
            });


            app.UseHttpsRedirection();

            app.UseRouting();
           

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
