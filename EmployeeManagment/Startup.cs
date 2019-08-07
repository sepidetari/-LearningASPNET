using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagment.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagment
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {

            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // we use this to configure services for our application 
        //or more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("EmployeeDbConncetion")));

            services.AddMvc().AddXmlSerializerFormatters();
            // If someone ask for IEmployeeRepository interface create MockEmployeeRepository instance for that
            services.AddScoped<IEmployeeRepository,SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment()) { 
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 5
                };
            app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            } else if (env.IsProduction() || env.IsStaging() || env.IsEnvironment("UAT"))
            {
                app.UseExceptionHandler("/Error");
            }

            /*
            app.Use(async (context, next) =>
            {
                logger.LogInformation("MV1: Incoming Request");
                //await context.Response.WriteAsync("Hello from 1st Middleware!!");
                //await context.Response.WriteAsync(_config["MyKey"]);
                //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                await next();
                logger.LogInformation("MV1: Outgoing Response");
            });


            app.Use(async (context, next) =>
            {
                logger.LogInformation("MV2: Incoming Request");
                await next();
                logger.LogInformation("MV2: Outgoing Response");
            }); */

            /*
            // these two middleware can be repaced bz UseFileServer middleware
            DefaultFilesOptions defaultFilesOpetions = new DefaultFilesOptions();
            defaultFilesOpetions.DefaultFileNames.Clear();
            defaultFilesOpetions.DefaultFileNames.Add("foo.html");
            app.UseDefaultFiles(defaultFilesOpetions);
            app.UseStaticFiles(); */

            /*
            FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            app.UseFileServer(fileServerOptions); */

            app.UseStaticFiles();

            // it dearches for HomeController Index method and define default outing for our app
            //app.UseMvcWithDefaultRoute();

            //Conventional routing
            // the default conventional template id? => ? means optional controller=Home => with = we define default value
            /* app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            }); */

            //Attribute routing
            // 
            app.UseMvc();

            /*app.Run(async (context) =>
            {
         
                await context.Response.WriteAsync("Hello from 2nd Middleware!");
                //await context.Response.WriteAsync("Hosting Environment is: " + env.EnvironmentName);
                //logger.LogInformation("MV3: Request handeled and response produced");
                //throw new Exception("Some error proceessing the request");
            });*/
        }
    }
}
