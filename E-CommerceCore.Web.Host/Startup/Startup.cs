using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using E_CommerceCore.Core.Domain.Entities.Security;
using E_CommerceCore.Core.Domain.Repositories.Interfaces;
using E_CommerceCore.EntityFrameworkCore.Configuration;
using E_CommerceCore.EntityFrameworkCore.Repositories.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using E_CommerceCore.EntityFrameworkCore.Seed;
using System.Reflection;
using E_CommerceCore.Core.Managers.Products;
using E_CommerceCore.Core.Managers.Categories;

namespace E_CommerceCore.Web.Startup
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ECommerceDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("LocalDb")));
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ECommerceDbContext>()
                .AddDefaultTokenProviders();

            IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "http://dotnetdetail.net",
                    ValidIssuer = "http://dotnetdetail.net",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"))
                };
            });

            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IEfCoreProductRepository, EfCoreProductRepository>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IEfCoreCategoryRepository, EfCoreCategoryRepository>();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200",
                                        "*")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });

            services.AddOpenApiDocument(document =>
            {
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                }); // add OpenAPI v3 document

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            // Requires using System.Reflection;
            //var assembly = typeof(AuthenticateController).GetTypeInfo().Assembly;
            //services.AddMvc().AddApplicationPart(assembly).AddControllersAsServices();

            services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("E-CommerceCore.Web.Core")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for 
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            SeedDB.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi(); // serve documents (same as app.UseSwagger())
            app.UseSwaggerUi3(); // serve Swagger UI
            app.UseReDoc(options =>
            {
                options.Path = "/redoc";
                options.DocumentPath = "/swagger/v1/swagger.json";
            }); // serve ReDoc UI

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}