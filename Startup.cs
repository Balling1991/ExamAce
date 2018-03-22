using AutoMapper;
using ExamAce.Data;
using ExamAce.Data.Entities;
using ExamAce.Data.Providers;
using ExamAce.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ExamAce
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(cfg =>
            {
                cfg.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials();
                });
            });

            services.AddAutoMapper();
            services.AddTransient<IMailService, TestMailService>();
            services.AddTransient<ExamAceSeeder>();
            services.AddScoped<IExamAceRepository, ExamAceRepository>();
            services.AddMvc(opt =>
            {
                if (_env.IsProduction())
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserManagement", policy => policy.RequireClaim("manage_user"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("admin"));
                options.AddPolicy("User", policy => policy.RequireClaim("user"));
            });

            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ExamAceContext>()
            .AddDefaultTokenProviders();

            services.AddDbContext<ExamAceContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("ExamAceConnectionString"));
            });

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Events.OnRedirectToLogin = context =>
                {
                    context.Response.Headers["Location"] = context.RedirectUri;
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                //cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["TokenAuthentication:Issuer"],
                    ValidAudience = Configuration["TokenAuthentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenAuthentication:SecretKey"]))
                };

                cfg.Events = new JwtBearerEvents
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

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            ExamAceContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("CorsPolicy");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            ApiDbSeedData.Seed(userManager, roleManager).Wait();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMiddleware<TokenProviderMiddleware>();
            app.UseMiddleware<RefreshTokenProviderMiddleware>();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
