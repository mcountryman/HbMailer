using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

using HbMailer;
using HbMailer.Model;

namespace HbMailer.Service {
  public class AppCtx {
    public Setting Setting { get; set; }
    public SettingManager SettingManager { get; set; }
    public MailJobCtx MailJobCtx { get; set; }
    public MailJobManager MailJobManager { get; set; }
    public MailJobDispatcher MailJobDispatcher { get; set; }

    public AppCtx() {
      Setting = new Setting();
      SettingManager = new SettingManager(this);

      MailJobCtx = new MailJobCtx(this);
      MailJobManager = new MailJobManager(MailJobCtx);
      MailJobDispatcher = new MailJobDispatcher(MailJobCtx);
    }
  }
}
