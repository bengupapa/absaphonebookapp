using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using phonebookserver.data;
using System;

namespace phonebookserver
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => 
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddLogging();
            services.AddDbContext<PhoneBookDbContext>(option => 
            {
                option.UseSqlServer(_configuration["ConnectionStrings:PhoneBookDb"]);
            });
            services.AddSwaggerGen(config => 
            {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Phone Book API",
                    Version = "v1",
                    Description = "Provides phone book management services.",
                    Contact = new OpenApiContact
                    {
                        Name = "Papa Bengu",
                        Email = string.Empty,
                        Url = new Uri("https://localhost:5001"),
                    }
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200", "http://localhost:4200/createentry");
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                  });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseCors("MyAllowSpecificOrigins");
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Phone Book API");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
