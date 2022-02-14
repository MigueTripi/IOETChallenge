using Microsoft.Extensions.DependencyInjection;

namespace IOETChallenge.Business
{
    public static class IoCExtension
    {
        public static IServiceCollection WithBusinessBinding(this IServiceCollection services)
        {
            services.AddScoped<IEmployeePaymentBusiness, EmployeePaymentBusiness>();
            services.AddScoped<IEmployeePaymentDataWrapper, EmployeePaymentDataWrapper>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IPaymentCalculator, PaymentCalculator>();
            return services;
        }
    }
}
