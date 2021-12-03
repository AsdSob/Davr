using System;
using System.Collections.Generic;
using System.IO;
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
using NLog;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Davr.Vash
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            var conString = Configuration.GetSection("ConnectionStrings");
            services.Configure<AppSettings>(conString);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
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
            services.AddScoped<ICurrencyRateCBService, CurrencyRateCBService>();
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddHostedService<UpdateRateService>();

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
                new Currency() {Code = "860", Name = "УЗБЕКСКИЙ СУМ", Ccy = "UZS"}, 
                new Currency() {Code = "840", Name = "ДОЛЛАP США", Ccy = "USD"},
                new Currency() {Code = "978", Name = "ЕВРО", Ccy = "EUR"},
                new Currency() {Code = "826", Name = "ФУНТ СТЕPЛИНГОВ", Ccy = "GBP"},
                new Currency() {Code = "036", Name = "АВСТPАЛИЙСКИЙ ДОЛЛАP", Ccy = "AUD"},                
                new Currency() {Code = "124", Name = "КАНАДСКИЙ ДОЛЛАP", Ccy = "CAD"},                
                new Currency() {Code = "156", Name = "ЮАНЬ РЕНЛИЕНБИ", Ccy = "CNY"},
                new Currency() {Code = "208", Name = "ДАТСКАЯ КPОНА", Ccy = "DKK"},                
                new Currency() {Code = "352", Name = "ИСЛАНДСКАЯ КPОНА", Ccy = "ISK"},
                new Currency() {Code = "392", Name = "ЙЕНА ЯПОНСКАЯ", Ccy = "JPY"},
                new Currency() {Code = "398", Name = "КАЗАХСКИЙ ТЕНГЕ", Ccy = "KZT"},
                new Currency() {Code = "410", Name = "ЮЖНОКОРЕЙСКИЙ ВОН", Ccy = "KRW"},
                new Currency() {Code = "414", Name = "КУВЕЙТСКИЙ ДИНАP", Ccy = "KWD"},
                new Currency() {Code = "417", Name = "КЫРГЫЗСКИЙ СОМ", Ccy = "KGS"},
                new Currency() {Code = "422", Name = "ЛИВАНСКИЙ ФУНТ", Ccy = "LBP"},
                new Currency() {Code = "458", Name = "МАЛАЙЗИЙСКИЙ PИНГГИТ", Ccy = "MYR"},
                new Currency() {Code = "643", Name = "РОССИЙСКИЙ РУБЛЬ", Ccy = "RUB"},
                new Currency() {Code = "702", Name = "СИНГАПУPСКИЙ ДОЛЛАP", Ccy = "SGD"},
                new Currency() {Code = "752", Name = "ШВЕДСКАЯ КPОНА", Ccy = "SEK"},
                new Currency() {Code = "756", Name = "ШВЕЙЦАPСКИЙ ФPАНК", Ccy = "CHF"},
                new Currency() {Code = "784", Name = "ДИPХАМ ОАЭ", Ccy = "AED"},                
                new Currency() {Code = "949", Name = "НОВАЯ ТУРЕЦКАЯ ЛИPА", Ccy = "TRY"},
                new Currency() {Code = "934", Name = "ТУРКМЕНСКИЙ МАНАТ", Ccy = "TMT"},
                new Currency() {Code = "818", Name = "ЕГИПЕТСКИЙ ФУНТ", Ccy = "EGP"},
                new Currency() {Code = "980", Name = "УКРАИНСКАЯ ГРИВНЯ", Ccy = "UAH"}, 
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
