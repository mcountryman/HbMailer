using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using NLog;
using NLog.Config;
using NLog.Targets;

using HbMailer.Jobs;
using HbMailer.Jobs.Dispatcher;
using HbMailer.Win32;

namespace HbMailer {
  class Program {
    static Logger         _logger;
    static Settings       _settings;
    static MailJobContext _mailJobCtx;
    static MailJobManager _mailJobManager;

    static void Main(string[] arguments) {
      SetupLogger();
      SetupEnvironment();

      if (arguments.Length <= 0) {
        _logger.Fatal("Started job runner without indicating a job to run!");
        return;
      }
    }

    static void SetupLogger() {
      var config = new LoggingConfiguration();
      var consoleTarget = new ColoredConsoleTarget("console") {
        Layout = "[${shortdate}][${level}][${logger}] ${message}",
      };
      var eventLogTarget = new EventLogTarget("eventLog") {
        Source = "HbMailer",
        Layout = "${message]${newline}${exception:format=ToString}",
      };

      config.AddTarget(consoleTarget);
      config.AddTarget(eventLogTarget);
      config.AddRuleForAllLevels(consoleTarget);
      config.AddRule(LogLevel.Info, LogLevel.Fatal, eventLogTarget);

      LogManager.Configuration = config;
    }

    static void SetupEnvironment() {
      _logger = LogManager.GetLogger("HbMailer");
      _settings = Settings.LoadSafe("HbMailer.xml");
      _mailJobCtx = new MailJobContext() { Settings = _settings };
      _mailJobManager = new MailJobManager(_mailJobCtx);
    }
  }
}
