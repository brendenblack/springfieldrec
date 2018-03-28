using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using HtmlTags;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpringfieldRecMvc.Infrastructure;
using SpringfieldRecMvc.Infrastructure.Tags;

namespace SpringfieldRecMvc
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
            services.AddMvc(opt => 
            {
                opt.Filters.Add(typeof(DbContextTransactionFilter));
                opt.Filters.Add(typeof(ValidatorActionFilter));
            })
            .AddFeatureFolders()
            .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>();  });

            services.AddHtmlTags(new TagConventions());


            //services.AddAutoMapper(typeof(Startup));
            //Mapper.AssertConfigurationIsValid();

            services.AddMediatR(typeof(Startup));

            string databaseFile = Configuration["Data:DefaultConnection:Filename"];
            services.AddEntityFrameworkSqlite().AddDbContext<SpringfieldRecContext>(opt => opt.UseSqlite($"Filename={databaseFile}"));

            var db = services.BuildServiceProvider().GetService<SpringfieldRecContext>();
            Seed.Initialize(db);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
