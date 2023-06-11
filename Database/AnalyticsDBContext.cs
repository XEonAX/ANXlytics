using System.Data;
using Configurations;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace Database;

public class AnalyticsDBContext
{
    private readonly string _connectionString;
    public AnalyticsDBContext(IOptions<AnalyticsDBConfiguration> dbOptions)
    {
        _connectionString = dbOptions.Value.ConnectionString;
    }
    public IDbConnection CreateConnection()
        => new SqliteConnection(_connectionString);
}