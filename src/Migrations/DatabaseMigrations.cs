using System.Reflection;
using DbUp;

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

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine("Migrations failed");
            Console.WriteLine(result.Error);
        }

        Console.WriteLine("Migrations successful");
    }
}
