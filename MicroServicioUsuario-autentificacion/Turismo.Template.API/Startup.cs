using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlKata.Compilers;
using Turismo.Template.AccessData.Command;
using Turismo.Template.AccessData.Context;
using Turismo.Template.AccessData.Queries;
using Turismo.Template.Application.Services;
using Turismo.Template.Domain.Commands;
using Turismo.Template.Domain.Queries;


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
            services.AddControllers();
            //var connectionString = Configuration.GetSection("ConnectionString").Value; //busca las configuraciones del sistema
            //Guardo conexion en variable
            var conexion = @"server=localhost;database=MsUser;trusted_connection=True;";

            services.AddDbContext<DbContextGeneric>(options => options.UseSqlServer(conexion));
            services.AddTransient<IRepositoryGeneric, GenericsRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRollServices, RollServices>();
            services.AddTransient<IPasajeroServices, PasajeroServices>();
            services.AddTransient<IEmpleadoServices, EmpleadoServices>();
            services.AddTransient<IMetodoPagoServices, MetodoPagoServices>();
            services.AddTransient<IUserQuery, UserQuery>();
            services.AddTransient<IPasajeroQuery, PasajeroQuery>();
            services.AddTransient<IEmpleadoQuery, EmpleadoQuery>();
            services.AddTransient<IRollQuery, RollQuery>();

            //Se agrega en generador de Swagger
            AddSwagger(services);

            //SQLKATA
            services.AddTransient<Compiler, SqlServerCompiler>();
            services.AddTransient<IDbConnection>(c =>
            {
                return new SqlConnection(conexion);
            });

            services.AddCors(c => c.AddDefaultPolicy(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            // Validacion JWT
            var key = Encoding.ASCII.GetBytes("xecretKeywqejane");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.getUserId(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Microservicio Usuario {groupName}",
                    Version = groupName,
                    Description = "Microservicio Usuario API",
                    Contact = new OpenApiContact
                    {
                        Name = "Projecto Software",
                        Email = string.Empty,
                        Url = new Uri("https://algo.com/"),
                    }
                });

            });
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //CORS
            app.UseCors();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            //indica la ruta para generar la configuración de swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prototype API V1");
            });


        }

    }
}