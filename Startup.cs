using AspNetCoreRateLimit;
using Funda.Configuration;
using Funda.Services;
using Funda.Services.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Funda {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      // needed to load configuration from appsettings.json
      services.AddOptions();

      // needed to store rate limit counters and ip rules
      services.AddMemoryCache();

      //load general configuration from appsettings.json
      services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

      // inject counter and rules stores
      services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
      services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

      // Add framework services.
      services.AddMvc();

      // the IHttpContextAccessor service is not registered by default.
      // the clientId/clientIp resolvers use it.
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      // configuration (resolvers, counter key builders)
      services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

      services.AddControllersWithViews();
      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration => {
        configuration.RootPath = "ClientApp/dist";
      });

      // Add ResponseCaching
      services.AddResponseCaching();

      // Resolve service dependencies
      services.AddScoped<IMakelaarService,MakelaarService>();
      services.AddScoped<IMakelaarServiceClient, MakelaarServiceClient>();
      services.AddScoped<IFundaConfiguration, FundaConfiguration>();
      services.AddScoped<IMakelaarsResponseMapper, MakelaarsResponseMapper>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }
      else {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseResponseCaching();
      app.UseIpRateLimiting();
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      if (!env.IsDevelopment()) {
        app.UseSpaStaticFiles();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa => {
        // Add the source path for serving the Angular single page application from Asp .net core
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment()) {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });
    }
  }
}
