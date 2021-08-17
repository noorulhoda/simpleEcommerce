
using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tachy1.BusinessLogicLayer.Services;
using tachy1.BusinessLogicLayer.Services.Interfaces;
using tachy1.Models;
using tachy1.Models.Interfaces;
using AspNetCore.Identity;
using IdentityModel;
using tachy1.Infra;

namespace tachy1
{
    public class Startup
    {
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services )

        {

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings)));
           

            ///////////////////////////////////////////////////

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options => {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,

                          ValidIssuer = Configuration.GetSection("Authentication:Issuer").Value,
                          ValidAudience = Configuration.GetSection("Authentication:Audience").Value,
                          IssuerSigningKey = JwtSecurityKey.Create(Configuration.GetSection("Authentication:SecurityKey").Value)
                      };

                      options.Events = new JwtBearerEvents
                      {
                          OnAuthenticationFailed = context =>
                          {
                              Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                              return Task.CompletedTask;
                          },
                          OnTokenValidated = context =>
                          {
                              Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                              return Task.CompletedTask;
                          }
                      };
                  });



            //////////////////////////////////////////////////////////////////////////

            //services.AddIdentityCore<ApplicationUser>(
            //    identityOptions =>
            //    {
            //        // Password settings.
            //        identityOptions.Password.RequiredLength = 6;
            //        identityOptions.Password.RequireLowercase = true;
            //        identityOptions.Password.RequireUppercase = true;
            //        identityOptions.Password.RequireNonAlphanumeric = false;
            //        identityOptions.Password.RequireDigit = true;

            //        // Lockout settings.
            //        identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //        identityOptions.Lockout.MaxFailedAccessAttempts = 5;
            //        identityOptions.Lockout.AllowedForNewUsers = true;

            //        // User settings.
            //        identityOptions.User.AllowedUserNameCharacters =
            //          "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //        identityOptions.User.RequireUniqueEmail = true;
            //    });  

            //// This is required to ensure server can identify user after login
            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //    options.UserNamePath = "/Identity/Account/UserName";
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});
            //// Add Jwt Authentication
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            //services.AddAuthentication(options =>
            //{
            //    //Set default Authentication Schema as Bearer
            //    options.DefaultAuthenticateScheme =
            //               JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme =
            //               JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme =
            //               JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(cfg =>
            //{
            //    cfg.RequireHttpsMetadata = false;
            //    cfg.SaveToken = true;
            //    cfg.TokenValidationParameters =
            //           new TokenValidationParameters
            //           {
            //               ValidIssuer = Configuration["JwtIssuer"],
            //               ValidAudience = Configuration["JwtIssuer"],
            //               IssuerSigningKey =
            //            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
            //               ClockSkew = TimeSpan.Zero // remove delay of token when expire
            //           };
            //});

            ///////////////////////////////////////////////////////////////////



            services.AddAuthorization(options =>
            {
                options.AddPolicy("User",
                    policy => policy.RequireClaim("MembershipId"));
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim("AdminId"));
                
            });


            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IAuthService, AuthService>();


           


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
           
    
        }








    }
}
