using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MonefyWeb.ApplicationService.Application.Tests
{
    public class TestStartup
    {
        public IConfiguration Configuration { get; }

        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IMonefyApplicationService, MonefyApplicationService>();
            //services.AddScoped<IMonefyDomain, MonefyDomain>();
            //services.AddSingleton<ILog>();
            //services.AddSingleton(Configuration);
        }
    }
}
