using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace WorldCities
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddJsonFile(string.Format("appsettings.{0}.json",
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                        ?? "Production"),
                    optional: true,
                    reloadOnChange: true)
                .AddUserSecrets<Startup>(optional: true, reloadOnChange: true)
                .Build();
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.PostgreSQL(
                   connectionString:
                       configuration.GetConnectionString("PostgreSqlConnection"),
                   tableName: "LogEvents",
                   needAutoCreateTable: true,
                   restrictedToMinimumLevel: LogEventLevel.Information
                   ).WriteTo.Console()
                   .CreateLogger();
                        
            CreateHostBuilder(args).UseSerilog().Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
