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
                    BranchId = 1
                });

            context.Users.Add(
                new User
                {
                    FirstName = "User",
                    LastName = "Userovich",
                    Username = "user",
                    PasswordHash = BCryptNet.HashPassword("user"),
                    Role = Role.User,
                    BranchId = 1
                });

            context.Users.Add(
                new User
                {
                    FirstName = "Super",
                    LastName = "Superov",
                    Username = "super",
                    PasswordHash = BCryptNet.HashPassword("super"),
                    Role = Role.Supervisor,
                    BranchId = 1
                });

            context.Branches.AddRange(CreateBranches());
            context.Currencies.AddRange(CreateCurrencies());
            context.Citizens.AddRange(CreateCitizen());  
            context.DocumentTypes.AddRange(CreateDocumentType());

            context.SaveChanges();

            context.Clients.AddRange(CreateTestClients());

            context.SaveChanges();
        }

        private List<Branch> CreateBranches()
        {
            var branches = new List<Branch>()
            {
                new Branch() {Name = "Golovnoy"},
                new Branch() {Name = "Olmazor"}
            };

            return branches;
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
        private List<Citizen> CreateCitizen()
        {
            var citizens = new List<Citizen>()
            {
                new Citizen()
                {
                    Name = "Узбекистан"
                },new Citizen()
                {
                    Name = "Россия"
                }
            };

            return citizens;
        }
        private List<DocumentType> CreateDocumentType()
        {
            var documentTypes = new List<DocumentType>()
            {
                new DocumentType() {Name = "Паспорт"},                
                new DocumentType() {Name = "Загран Пасспорт"},
                new DocumentType() {Name = "идентификационный документ"},
            };

            return documentTypes;
        }

        private List<Client> CreateTestClients()
        {
            var newClients = new List<Client>()
            {
                new Client()
                {
                    Name = "Maruf",
                    SurName = " Abduvoqidov",
                    MiddleSurName = "Sirojev",
                    BirthDate = new DateTime(1991, 7, 20),
                    BirthPlace = "Toshkent",
                    DocumentIssueDate =  new DateTime(2010, 7, 3),
                    DocumentAuthority = "IIB Toshkent",
                    DocumentNumber = "123",
                    DocumentSeries = "AA",
                    DocumentTypeId = 1,
                    Registration = "Usman Nosir 12-24",
                    CitizenId = 2
                },
                new Client()
                {
                    Name = "Eshon",
                    SurName = " Eshonov",
                    MiddleSurName = "Eshon ugli",
                    BirthDate = new DateTime(1989, 1, 1),
                    BirthPlace = "Toshkent",
                    DocumentIssueDate =  new DateTime(2012, 2, 23),
                    DocumentAuthority = "IIB Toshkent",
                    DocumentNumber = "321",
                    DocumentSeries = "AB",
                    DocumentTypeId = 2,
                    Registration = "Olmazor 55 dom, 4 kv",
                    CitizenId = 1
                },

                new Client()
                {
                    Name = "Saidov",
                    SurName = "Popov",
                    MiddleSurName = "Petrovich",
                    BirthDate = new DateTime(1980, 9, 3),
                    BirthPlace = "Buxoro",
                    DocumentIssueDate =  new DateTime(2020, 1, 10),
                    DocumentAuthority = "IIB Fargona",
                    DocumentNumber = "0000",
                    DocumentSeries = "BB",
                    DocumentTypeId = 1,
                    Registration = "Lininskaya, 2 kv, dom 123-45",
                    CitizenId = 1
                }
            };

            return newClients;
        }
    }
}
