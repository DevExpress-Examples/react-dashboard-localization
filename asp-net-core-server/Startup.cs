using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreDashboardBackend {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) {
            Configuration = configuration;
            FileProvider = hostingEnvironment.ContentRootFileProvider;
        }

        public IConfiguration Configuration { get; }
        public IFileProvider FileProvider { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Configures services to use the Web Dashboard Control.
            services
                .AddCors(options => {
                    options.AddPolicy("CorsPolicy", builder => {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.WithHeaders("Content-Type");
                    });
                })
                .AddDevExpressControls()
                .AddControllers()
                .AddDefaultDashboardController(configurator => {
                    configurator.SetDashboardStorage(new DashboardFileStorage(FileProvider.GetFileInfo("App_Data/Dashboards").PhysicalPath));
                    configurator.SetDataSourceStorage(CreateDataSourceStorage());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            var supportedCultures = new[] { "en-US", "de-DE" };
            var opts = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[1])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            opts.RequestCultureProviders.Clear();
            app.UseRequestLocalization(opts);

            // Registers the DevExpress middleware.
            app.UseDevExpressControls();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints => {
                // Maps the dashboard route.
                EndpointRouteBuilderExtension.MapDashboardRoute(endpoints, "api/dashboard");
                // Requires CORS policies.
                endpoints.MapControllers().RequireCors("CorsPolicy");
            });
        }

        public DataSourceInMemoryStorage CreateDataSourceStorage() {
            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            DashboardObjectDataSource objDataSource = new DashboardObjectDataSource("ObjectDataSource", typeof(ProductSales));
            objDataSource.DataMember = "GetProductSales";
            dataSourceStorage.RegisterDataSource("objectDataSource", objDataSource.SaveToXml());
            return dataSourceStorage;
        }
    }
}