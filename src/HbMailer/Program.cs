using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using NLog;
using NLog.Config;
using NLog.Targets;

using HbMailer.Model;
using HbMailer.Service;
using HbMailer.Properties;

namespace HbMailer {
  class Program {
    static void SetupLogger() {
      var config = new LoggingConfiguration();
      var fileTarget = new FileTarget("file") {
        Layout = @"[${longdate}][${level}] ${message} ${exception:format=message}",
        FileName = Path.Combine(Resources.LogFolder, "error.log"),
        ArchiveEvery = FileArchivePeriod.Day,
        ArchiveAboveSize = 10240,
        ArchiveNumbering = ArchiveNumberingMode.Date,
      };
      var consoleTarget = new ColoredConsoleTarget("console") {
        Layout = @"[${date:format=HH\:mm\:ss}][${level}] ${message} ${exception:format=toString}",
      };

      config.AddTarget(fileTarget);
      config.AddTarget(consoleTarget);

      // Only allow logs more severe or equally severe as Warning to file
      config.AddRuleForOneLevel(LogLevel.Warn, fileTarget);
      // Allow all logs to console
      config.AddRuleForAllLevels(consoleTarget);

      // Activate configuration
      LogManager.Configuration = config;
    }

    static void Run() {
      AppCtx ctx = new AppCtx();

      if (!ctx.SettingManager.TryLoad(Resources.SettingsFile)) return;
      if (!ctx.MailJobManager.TryInitialize()) return;
      if (!ctx.MailJobDispatcher.TryInitialize()) return;
    }

    static void Main() {
      SetupLogger();
      Run();

      #if DEBUG
      Console.Write("Press any key to continue...");
      Console.ReadKey();
      #endif
    }
  }
}
