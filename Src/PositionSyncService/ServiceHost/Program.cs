using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ServiceHost
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MainService()
            };

            if (args.Contains("-c")) {
                ((MainService)ServicesToRun[0]).DebugStart(args);

                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite); //System.Threading.Timeout.Infinite, 5000
                ((MainService)ServicesToRun[0]).DebugStop();
                return;
            }

            ServiceBase.Run(ServicesToRun);
        }
    }
}
