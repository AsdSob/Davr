using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using Davr.Gumon.Authorization;
using Davr.Gumon.DataAccess;
using Davr.Gumon.Entities;
using Davr.Gumon.Helpers;
using Davr.Gumon.Services;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;


namespace Davr.Gumon
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
            services.AddDbContext<DataContext>();
            services.Configure<AppSettings>(Configuration.GetSection("ConnectionStrings"));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:4200", "http://mywebsite.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });



            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // configure DI for application services
            services.AddScoped<IDataAccessProvider, DataAccessProvider>();
            services.AddScoped<IPageResponseService, PageResponseService>();
            services.AddScoped<IFieldFilter, FieldFilter>();
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Davr.Vash", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Davr.Vash v1"));
            }

            app.UseHttpsRedirection();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();
            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseRouting();

            app.UseCors("CorsApi");

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            if (!dataContext.Users.Any())
            {
                CreateBaseData(dataContext);
            }
        }

        private void CreateBaseData(DataContext context)
        {
            context.Users.Add(
                new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Username = "admin",
                    PasswordHash = BCryptNet.HashPassword("admin"),
                    Role = Role.Admin
                });

            context.SaveChanges();
        }
    }
}
