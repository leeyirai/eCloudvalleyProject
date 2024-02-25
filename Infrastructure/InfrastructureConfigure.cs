using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class InfrastructureConfigure
    {
        public static void AddService(IServiceCollection service)
        {
            service.AddScoped<ISqliteInfra, SqliteInfra>();
        }
    }
}
