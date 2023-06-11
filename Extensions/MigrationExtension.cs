
using System.Reflection;
using Configurations;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

namespace Extensions;
public static class MigrationExtension
{
    public static void Migrate(string connectionStringAnalytics)
    {
        var serviceProviderDb1 = new ServiceCollection()
            .AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionStringAnalytics)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()
                )
                .Configure<RunnerOptions>(opt =>
                {
                    opt.Tags = new[] { "Analytics" };
                })
                .BuildServiceProvider(false);

        using (var scope = serviceProviderDb1.CreateScope())
        {
            var runner = serviceProviderDb1.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

    }
}