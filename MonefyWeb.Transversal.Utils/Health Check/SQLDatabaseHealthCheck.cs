using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MonefyWeb.Transversal.Utils.Health_Check
{
    public class SQLDatabaseHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public SQLDatabaseHealthCheck(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("SQLDatabase");
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using SqlConnection cn = new(_connectionString);
                cn.Open();
                if (cn is null)
                    return HealthCheckResult.Healthy("Database connection is open.");

                return HealthCheckResult.Unhealthy("Database connection is closed.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Exception: {ex.Message}");
            }
        }
    }
}
