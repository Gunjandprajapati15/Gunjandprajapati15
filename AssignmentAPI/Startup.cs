using AngularApplicationTest.DataAccess;
using AngularApplicationTest.Repositories;
using AngularApplicationTest.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AngularApplicationTest
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
           
            services.AddCors(service =>
                    service.AddPolicy("CORSPolicy", option => {
                        option.AllowAnyOrigin();
                        option.AllowAnyMethod();
                        option.AllowAnyHeader();
                    })
                );

            // Setup context
            String connectionString = Configuration["ConnectionString"];
            if (String.IsNullOrEmpty(connectionString))
            {
                connectionString = "Data Source=servername;Initial Catalog=dbName;Integrated Security=False;User Id=sa;Password=Password;MultipleActiveResultSets=True;";
            }
            var connection = @connectionString;

            services.AddDbContextPool<Context>(options => options.UseSqlServer(connection,
                 opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(1).TotalSeconds)));

            //services.AddMvc(options => { options.EnableEndpointRouting = false; }).AddJsonOptions(options => 
            //{
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //}); 
            services.AddMvc(options => { options.EnableEndpointRouting = false; });

            services.AddScoped<ILinkedInRepository, LinkedInRepository>();
            services.AddScoped<ILinkedInService, LinkedInService>();
            services.AddSingleton<IStudentRepository, StudentRepository>();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName.ToLower() == "development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseCors("CORSPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
            
        }
    }
}
