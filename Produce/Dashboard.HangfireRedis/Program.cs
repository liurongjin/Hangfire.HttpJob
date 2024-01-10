using Microsoft.AspNetCore.Hosting;
using NLog;
using NLog.Web;

namespace Dashboard.HangfireRedis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings();
            try
            {
                System.Threading.ThreadPool.SetMinThreads(200, 200); //设置全局线程池
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.GetCurrentClassLogger().Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureLogging(logging =>
                        {
                            logging.ClearProviders();

#if DEBUG
                            logging.AddConsole();
                            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);

#else
                            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);
#endif
                        });
                });
    }
}