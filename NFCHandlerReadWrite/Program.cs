using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NFCHandlerReadWrite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new NfcReadWriteService()
            };
            ServiceBase.Run(ServicesToRun);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
                  Host.CreateDefaultBuilder(args)
                    .ConfigureServices(services =>
                    {
                        services.Configure<EventLogSettings>(config =>
                        {
                            config.LogName = "Sample API Service";
                            config.SourceName = "Sample API Service Source";
                        });
                    });
                     
    }
}
