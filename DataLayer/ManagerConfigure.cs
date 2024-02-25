using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{
    public class ManagerConfigure
    {
        public static void AddService(IServiceCollection service)
        {
            service.AddScoped<IBillingManager, BillingManager>();
        }
    }
}
