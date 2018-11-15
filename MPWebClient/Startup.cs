using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MPWebClient
{
    public class Startup
    {
        private IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            HostingEnvironment = env;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc();

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddNodeServices(options =>
                {
                    
                    options.LaunchWithDebugging = true;
                    options.DebuggingPort = 9229;
                    
                });
            }

            services.AddOptions();

            services.AddResponseCompression();

            services.AddMemoryCache();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.AddDevMiddlewares();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseHsts();
                app.UseResponseCompression();
            }

            app.UseStaticFiles();


            app.UseSpaStaticFiles();

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                //routes.MapSpaFallbackRoute(
                //    name: "spa-fallback",
                //    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(name: "hello", template: "hello", defaults: new { controller = "Home", action = "Hello" });
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                
                /*
                // If you want to enable server-side rendering (SSR),
                // [1] In AspNetCoreSpa.csproj, change the <BuildServerSideRenderer> property
                //     value to 'true', so that the SSR bundle is built during publish
                // [2] Uncomment this code block
                */

                //spa.UseSpaPrerendering(options =>
                // {
                //     options.BootModulePath = $"{spa.Options.SourcePath}/dist-server/main.bundle.js";
                //     options.BootModuleBuilder = env.IsDevelopment() ? new AngularCliBuilder(npmScript: "build:ssr") : null;
                //     options.ExcludeUrls = new[] { "/sockjs-node" };
                //     options.SupplyData = (requestContext, obj) =>
                //     {
                //            //  var result = appService.GetApplicationData(requestContext).GetAwaiter().GetResult();
                //            obj.Add("Cookies", requestContext.Request.Cookies);
                //     };
                // });

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start-client");
                    spa.UseAngularCliServer(npmScript: "start-admin");
                    //   OR
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
