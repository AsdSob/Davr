using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Davr.Vash.Authorization;
using Davr.Vash.DataAccess;
using Davr.Vash.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Davr.Vash.Helpers;
using Davr.Vash.Services;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Davr.Vash
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
                    Role = Role.Admin,
                    Branch = new Branch()
                    {
                        Name = "Golovnoy"
                    }
                });

            var currencies = CreateCurrencies();

            context.Currencies.AddRange(currencies);

            context.SaveChanges();
        }


        private List<Currency> CreateCurrencies()
        {
            var currencyList = new List<Currency>()
            {
                new Currency()
                {
                    Code = "860",
                    Name = "УЗБЕКСКИЙ СУМ"
                }, new Currency()
                {
                    Code = "840",
                    Name = "ДОЛЛАP США"
                },new Currency()
                {
                    Code = "978",
                    Name = "ЕВРО"
                }, new Currency()
                {
                    Code = "826",
                    Name = "ФУНТ СТЕPЛИНГОВ"
                },
                new Currency()
                {
                    Code = "36",
                    Name = "АВСТPАЛИЙСКИЙ ДОЛЛАP"
                },                
                new Currency()
                {
                    Code = "124",
                    Name = "КАНАДСКИЙ ДОЛЛАP"
                },                
                new Currency()
                {
                    Code = "156",
                    Name = "ЮАНЬ РЕНЛИЕНБИ"
                },                new Currency()
                {
                    Code = "208",
                    Name = "ДАТСКАЯ КPОНА"
                },                new Currency()
                {
                    Code = "352",
                    Name = "ИСЛАНДСКАЯ КPОНА"
                },                new Currency()
                {
                    Code = "392",
                    Name = "ЙЕНА ЯПОНСКАЯ"
                },                new Currency()
                {
                    Code = "398",
                    Name = "КАЗАХСКИЙ ТЕНГЕ"
                },                new Currency()
                {
                    Code = "410",
                    Name = "ЮЖНОКОРЕЙСКИЙ ВОН"
                },                new Currency()
                {
                    Code = "414",
                    Name = "КУВЕЙТСКИЙ ДИНАP"
                },                new Currency()
                {
                    Code = "417",
                    Name = "КЫРГЫЗСКИЙ СОМ"
                },                new Currency()
                {
                    Code = "422",
                    Name = "ЛИВАНСКИЙ ФУНТ"
                },                new Currency()
                {
                    Code = "458",
                    Name = "МАЛАЙЗИЙСКИЙ PИНГГИТ"
                },                new Currency()
                {
                    Code = "643",
                    Name = "РОССИЙСКИЙ РУБЛЬ"
                },                new Currency()
                {
                    Code = "702",
                    Name = "СИНГАПУPСКИЙ ДОЛЛАP"
                },                new Currency()
                {
                    Code = "752",
                    Name = "ШВЕДСКАЯ КPОНА"
                },                new Currency()
                {
                    Code = "756",
                    Name = "ШВЕЙЦАPСКИЙ ФPАНК"
                },                new Currency()
                {
                    Code = "784",
                    Name = "ДИPХАМ ОАЭ"
                },                new Currency()
                {
                    Code = "792",
                    Name = "ТУРЕЦКАЯ ЛИPА"
                },                new Currency()
                {
                    Code = "795",
                    Name = "ТУРКМЕНСКИЙ МАНАТ"
                },      new Currency()
                {
                    Code = "818",
                    Name = "ЕГИПЕТСКИЙ ФУНТ"
                },  new Currency()
                {
                    Code = "902",
                    Name = "ЧOMBE КОНГО ТРОПИЧЕСКАЯ"
                }, new Currency()
                {
                    Code = "949",
                    Name = "НОВАЯ ТУРЕЦКАЯ ЛИРА"
                },  new Currency()
                {
                    Code = "980",
                    Name = "УКРАИНСКАЯ ГРИВНЯ"
                }, 
            };

            return currencyList;
        }
    }
}
