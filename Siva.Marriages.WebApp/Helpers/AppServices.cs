
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Siva.Marriages.Business;
using Siva.Marriages.Business.DB;

namespace Siva.Marriages.WebApp.Helpers
{
    public static class AppServices
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<GDriveProvider>();
            services.AddDbContext<PGSqlDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetNpgsqlConnection());
            });
            services.AddTransient<ProfileOperations>();
        }

        private static string GetNpgsqlConnection(this IConfiguration configuration)
        {
            var builder = new NpgsqlConnectionStringBuilder();
            var dbUri = configuration.GetValue<Uri>("DATABASE_URL");
            if (dbUri == default)
                throw new ArgumentNullException("Without Database Connection, App can't be start!");
            var userInfo = dbUri.UserInfo.Split(':');
            builder.Username = userInfo[0];
            builder.Password = userInfo[1];
            builder.Host = dbUri.Host;
            builder.Port = dbUri.Port;
            builder.Database = dbUri.Segments[1];
            return builder.ConnectionString;
        }
    }
}
