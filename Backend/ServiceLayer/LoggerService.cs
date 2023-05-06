using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using log4net.Config;
using System.IO;
using log4net;
using Microsoft.Extensions.Logging;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class LoggerService
    {
        internal static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public LoggerService()
        {
            var logRespository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRespository, new FileInfo("log4net.config"));
            log.Info("starting log!");
        }
    }
}




