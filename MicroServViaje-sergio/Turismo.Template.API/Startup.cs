using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Turismo.Template.AccessData;
using Turismo.Template.AccessData.Command;
using Turismo.Template.AccessData.Queries;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.Queries;
using Turismo.Template.Domain.Services;

namespace Turismo.Template.API
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
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            //services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));
            //services.AddDbContext<DbContextGeneric>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(c => c.AddDefaultPolicy(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            var connectionString = Configuration.GetSection("DefaultConnection").Value;
            services.AddDbContext<DbContextGeneric>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IViajeRepository, ViajeRepository>();
            services.AddTransient<IViajeServices, ViajeServices>();

            services.AddTransient<ICoordinadorRepository, CoordinadorRepository>();
            services.AddTransient<ICoordinadorService, CoordinadorService>();

            services.AddTransient<IChoferRepository, ChoferRepository>();
            services.AddTransient<IChoferService, ChoferService>();

            services.AddTransient<IBusRepository, BusRepository>();
            services.AddTransient<IBusService, BusService>();

            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<IEmpresaService, EmpresaService>();

            services.AddTransient<IGrupoRepository, GrupoRepository>();
            services.AddTransient<IGrupoService, GrupoService>();

            services.AddTransient<ITerminalRepository, TerminalRepository>();
            services.AddTransient<ITerminalService, TerminalService>();

            services.AddControllers();
            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            //services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //AddSwagger(services);
            services.AddSwaggerGen();

        }

        //private void AddSwagger(IServiceCollection services)
        //{
        //    services.AddSwaggerGen(options =>
        //    {
        //        var groupName = "v1";

        //        options.SwaggerDoc(groupName, new OpenApiInfo
        //        {
        //            Title = $"Foo {groupName}",
        //            Version = groupName,
        //            Description = "Foo API",
        //            Contact = new OpenApiContact
        //            {
        //                Name = "Foo Company",
        //                Email = string.Empty,
        //                Url = new Uri("https://foo.com/"),
        //            }
        //        });
        //    });
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });


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
