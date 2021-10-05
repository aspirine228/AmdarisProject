using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using GameTracker.Services.Profiles;
using GameTracker.Rep.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.OpenApi.Models;
using GameTracker.Rep.Configurations;
using GameTracker.Services.Interfaces;
using GameTracker.Services.Services;
using GameTracker.Rep.Repositories;
using Microsoft.EntityFrameworkCore;
using GameTracker.Rep;
using GameTracker.Domain.Auth;
using GameTracker.API.Extensions;
using GameTracker.API.Infrastructure.Middleware;


namespace GameTracker.API
{
    public class Startup
    {         
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

    
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MyGameDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(Configuration.GetConnectionString("GamerTracker"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
            })
          .AddEntityFrameworkStores<MyGameDbContext>();

            var authOptions = services.ConfigureAuthOptions(Configuration);
            services.AddJwtAuthentication(authOptions);
            services.AddControllers();

           
            var mapperConfig = new MapperConfiguration(m =>
            {
               m.AddProfile(new GamerProfile());
               m.AddProfile(new GameProfile());
               m.AddProfile(new CompanyProfile());
               m.AddProfile(new NewsProfile());
               m.AddProfile(new FeedBackProfile());
            }
            );

            ConfigureSwagger(services);        
           
            services.AddSingleton(mapperConfig.CreateMapper());
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IGamerService, GamerService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<INewsService, NewsService>();

        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ErrorHandling>();              
            }

            app.UseHttpsRedirection();
            app.UseRouting();
          
            app.UseAuthentication();
            app.UseAuthorization();
          
                   
            app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowedToAllowWildcardSubdomains().WithOrigins());
       
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Swagger Demo API v1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });         
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var contact = new OpenApiContact()
            {
                Name = "FirstName LastName",
                Email = "user@example.com",
                Url = new Uri("http://www.example.com")
            };

            var license = new OpenApiLicense()
            {
                Name = "My License",
                Url = new Uri("http://www.example.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Swagger Demo API",
                Description = "Swagger Demo API Description",
                TermsOfService = new Uri("http://www.example.com"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);

                c.OperationFilter<SwaggerFileOperationFilter>();

            });
        }
    }
}
