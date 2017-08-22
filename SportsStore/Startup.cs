using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SportsStore
{
  public class Startup
  {
    IConfiguration Configuration;

    // Define constructor in order to use the appsettings.json file to load config settings
    public Startup(IHostingEnvironment env)
    {
      Configuration = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json").Build();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      // Configures the database for the db context class
      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
          Configuration["Data:SportStoreProducts:ConnectionString"]));
      services.AddTransient<IProductRepository, EFProductRepository>(); //FakeProductRepository>();
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      app.UseDeveloperExceptionPage();
      app.UseStatusCodePages();
      app.UseStaticFiles();
      //app.UseMvcWithDefaultRoute();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Product}/{action=List}/{id?}"); // MVC naming convention -- "Product" instead of "ProductName"
      });
      SeedData.EnsurePopulated(app);
    }
  }
}
