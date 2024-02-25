using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer
{
    public class LogicConfigure
    {
        public static void AddService(IServiceCollection service)
        {
            service.AddScoped<IBillingLogic, BillingLogic>();
        }
    }
}
