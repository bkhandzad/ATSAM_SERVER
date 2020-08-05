using System;
using System.ServiceProcess;

namespace AtsamServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[]
                {
                    new ATSAM_SERVER()
                };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
