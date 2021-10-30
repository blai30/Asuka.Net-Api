using System.Reflection;
using DbUp;
using Polly;

namespace Migrations;

public static class DatabaseMigrations
{
    public static void ExecuteScripts(string connectionString)
    {
        var upgrader = DeployChanges.To
            .MySqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryForever(_ => TimeSpan.FromSeconds(5));

        retryPolicy.Execute(() =>
        {
            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception();
            }
        });
    }
}
