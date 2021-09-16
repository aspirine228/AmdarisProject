using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using GameTracker.Services.Profiles;
using Microsoft.AspNetCore.HttpsPolicy;
using GameTracker.Rep.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using GameTracker.Rep.Configurations;
using GameTracker.Services.Interfaces;
using GameTracker.Services.Services;
using GameTracker.Rep.Repositories;
using Microsoft.EntityFrameworkCore;
using GameTracker.Rep;

namespace GameTracker.API

{
    public class Startup
    {
        private static void SomeTest1(IApplicationBuilder app)
        {
            app.Run(async context => { await context.Response.WriteAsync("This is Run Test"); });
        }
        public  class SomeTest2
        {
            private readonly RequestDelegate _next;
            public SomeTest2(RequestDelegate next)
            {
                _next = next;
            }
            public async Task Invoke(HttpContext context)
            {
                await  context.Response.WriteAsync("DoSome With Class");

                await _next.Invoke(context);

            }

        }
        //public static class MiddlewareExtensions
        //{
        //    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app) { return app.UseMiddleware<SomeTest2>(); }

        //}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyGameDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(Configuration.GetConnectionString("GamerTracker"));
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
           
            var mapperConfig = new MapperConfiguration(m =>
            {
               m.AddProfile(new GamerProfile());
            }
            );
            ConfigureSwagger(services);
            services.AddControllers();
            
            services.AddSingleton(mapperConfig.CreateMapper());
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IGamerService, GamerService>();
           // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            //
          //  app.UseAuthentication();
           // app.UseAuthorization();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Swagger Demo API v1");
            });

            //app.UseMiddleware<SomeTest2>();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //  app.Map("/test1", SomeTest1);

            //app.Run(async context => {  await context.Response.WriteAsync("Hello , just run after map with non map"); });
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
