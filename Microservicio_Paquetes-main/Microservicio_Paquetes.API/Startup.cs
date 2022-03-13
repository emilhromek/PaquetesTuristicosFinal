using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microservicio_Paquetes.Application.Services;
using Microservicio_Paquetes.Domain.Commands;
using Microservicio_Paquetes.Domain.Queries;
using Microservicio_Paquetes.AccessData.Commands;
using Microservicio_Paquetes.AccessData.Queries;
using Microservicio_Paquetes.AccessData;
using Microsoft.OpenApi.Models;

namespace Microservicio_Paquetes.API
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
            services.AddCors(c => c.AddDefaultPolicy(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddControllers();
            //var connectionString = Configuration.GetSection(@"Server=localhost;Database=master;Trusted_Connection=True;").Value; //busca las configuraciones del sistema
            services.AddDbContext<PaquetesDbContext>(options => options.UseSqlServer(@"Server=localhost;Database=mspaquetes;Trusted_Connection=True;"));

            services.AddTransient<ICommands, Commands>();
            services.AddTransient<IQueries, Queries>();

            services.AddTransient<IComentarioDestinoService, ComentarioDestinoService>();

            services.AddTransient<IPaqueteService, PaqueteService>();

            services.AddTransient<IHotelService, HotelService>();

            services.AddTransient<IDestinoService, DestinoService>();

            services.AddTransient<IExcursionService, ExcursionService>();

            services.AddTransient<IReservaService, ReservaService>();

            AddSwagger(services);
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Microservicio de paquetes {groupName}",
                    Version = groupName,
                    Description = "Paquetes turísticos - Microservicio de paquetes API",
                    Contact = new OpenApiContact
                    {
                        Name = "Paquetes turísticos",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservicio Paquete V1");
            });            
        }
    }
}
