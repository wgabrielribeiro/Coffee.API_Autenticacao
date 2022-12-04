using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Coffee.API.Data;
using Microsoft.EntityFrameworkCore;
using Coffee.API.Base;
using Microsoft.AspNetCore.Identity;
using Coffee.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Coffee.API
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
            methodDbConect methodDb = new methodDbConect();

            services.AddCors();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "EnableCORS",
            //                      policy =>
            //                      {
            //                          policy.WithOrigins("http://localhost:4200",
            //                              "http://localhost:4200/perfil",
            //                              "https://localhost:44384", "http://192.168.10.223:40")
            //                          .AllowAnyHeader()
            //                          .AllowAnyMethod().AllowAnyOrigin();
            //                      });
            //});

            //services.AddDbContext<AppDbContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(methodDb.fnReadXML("CoffeeUsers")));

            services.AddDbContext<AppDbContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer("Server=tcp:coffeeusers.database.windows.net,1433;Initial Catalog=CoffeeUsers;Persist Security Info=False;User ID=desenvolvimento;Password=G@briel1010;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
                opt =>
                {
                    opt.SignIn.RequireConfirmedEmail = true;
                    opt.User.RequireUniqueEmail = true;

                    opt.Lockout.AllowedForNewUsers = true;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    opt.Lockout.MaxFailedAccessAttempts = 3;

                }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var chave = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("ushdisbdiusabdubsadi08sadas44asd85s4dsw84"));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = chave,
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });


            services.AddScoped<InsercoesServices, InsercoesServices>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<EmailService, EmailService>();
            services.AddScoped<EmailConfigReadXML, EmailConfigReadXML>();
            services.AddScoped<LogoutService, LogoutService>();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddNewtonsoftJson();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coffee.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coffee.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader().AllowAnyOrigin());

            //app.UseCors("EnableCORS");

            //app.UseCors(x => x.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader().WithOrigins("http://localhost:4200", "http://localhost:4200"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
