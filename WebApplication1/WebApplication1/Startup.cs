using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;
using MessengerAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

/*
 * Main file for the project. Adds services and configures them. 
 * Adds database services and services for swagger.
 * Reference for swagger: https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger
 * */
namespace MessengerAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. 
        public void ConfigureServices(IServiceCollection services)
        {
            // adds the database services
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<BooksAPIContext>(x =>
            {
                x.UseNpgsql("Server=localhost;Database=postgres;User Id=postgres;Password=Waheguru@77",
                    b => b.MigrationsAssembly("MessengerAPI"));
               // x.UseInMemoryDatabase();
            });

            // adds swagger services for API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("first", new Info { Title = "MessengerAPI", Version = "first" });
            });
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRespository, AuthorRepository>();
            services.AddTransient<ILibraryRepository, LibraryRepository>();
            services.AddTransient<IPatronRepository, PatronRepository>();
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(o=>o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. 
        // sets the swagger to view the API.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            // configures the swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/first/swagger.json", "MessengerAPI");
            });
        }
    }
}
