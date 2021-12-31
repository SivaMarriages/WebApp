using Siva.Marriages.Business;

namespace Siva.Marriages.WebApp.Helpers
{
    public static class AppServices
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<GDriveProvider>();
        }
    }
}
