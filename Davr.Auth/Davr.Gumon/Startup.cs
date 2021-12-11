using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
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
            services.AddCors();

            var conString = Configuration.GetSection("ConnectionStrings");
            services.Configure<AppSettings>(conString);

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
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Davr.Gumon", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "Davr.Gumon v2"));
            }

            app.UseRouting();
            app.UseCors(x => x.AllowAnyMethod().SetIsOriginAllowed(x => true).AllowAnyHeader());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

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
                    MiddleName = "--",
                    LastName = "Adminov",
                    Username = "admin",
                    PasswordHash = BCryptNet.HashPassword("admin"),
                    Role = Role.Admin,
                    BranchId = 1
                });
            context.Users.Add(
                new User
                {
                    FirstName = "Supervisor",
                    MiddleName = "--",
                    LastName = "Superviserovich",
                    Username = "super",
                    PasswordHash = BCryptNet.HashPassword("super"),
                    Role = Role.Supervisor,
                    BranchId = 1
                });
            context.Users.Add(
                new User
                {
                    FirstName = "User",
                    MiddleName = "--",
                    LastName = "Userovich",
                    Username = "user",
                    PasswordHash = BCryptNet.HashPassword("user"),
                    Role = Role.User,
                    BranchId = 1
                });

            context.Branches.AddRange(GenerateBranches());
            context.Currencies.AddRange(GenerateCurrencies());
            context.TransactionStatuses.AddRange(GenerateTransactionStatus());
            context.OperationCriterias.AddRange(GenerateOperationCreatias());

            context.SaveChanges();
        }

        private List<Branch> GenerateBranches()
        {
            var branches = new List<Branch>()
            {
                new Branch() {Name = "Golovnoy", Code = "1001"},
                new Branch() {Name = "Olmazor", Code = "1001"}
            };

            return branches;
        }

        private List<Currency> GenerateCurrencies()
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

        private List<TransactionStatus> GenerateTransactionStatus()
        {
            var statuses = new List<TransactionStatus>()
            {
                new TransactionStatus(){Name = "Утвержден"},
                new TransactionStatus(){Name = "Введен"},
            };

            return statuses;
        }

        private List<OperationCriteria> GenerateOperationCreatias()
        {
            var items = new List<OperationCriteria>()
            {
                new OperationCriteria(){Id= 1, Code = "501/1"},
                new OperationCriteria(){Id= 2, Code = "501/2"},
                new OperationCriteria(){Id= 3, Code = "502"},
                new OperationCriteria(){Id= 5, Code = "503"},
                new OperationCriteria(){Id= 7, Code = "504"},
                new OperationCriteria(){Id= 8, Code = "505"},
                new OperationCriteria(){Id= 9, Code = "506"},
                new OperationCriteria(){Id= 10, Code = "507"},
                new OperationCriteria(){Id= 11, Code = "508"},
                new OperationCriteria(){Id= 12, Code = "509"},
                new OperationCriteria(){Id= 13, Code = "510"},
                new OperationCriteria(){Id= 14, Code = "511"},
                new OperationCriteria(){Id= 15, Code = "512"},
                new OperationCriteria(){Id= 16, Code = "513"},
                new OperationCriteria(){Id= 17, Code = "514"},
                new OperationCriteria(){Id= 18, Code = "515"},
                new OperationCriteria(){Id= 19, Code = "516/1"},
                new OperationCriteria(){Id= 20, Code = "516/2"},
                new OperationCriteria(){Id= 21, Code = "517/1"},
                new OperationCriteria(){Id= 22, Code = "517/2"},
                new OperationCriteria(){Id= 23, Code = "518"},
                new OperationCriteria(){Id= 24, Code = "519"},
                new OperationCriteria(){Id= 25, Code = "520"},
                new OperationCriteria(){Id= 26, Code = "521"},
            };

            var ordered = items.OrderBy(x => x.Code);

            return ordered.ToList();
        }
    }
}
