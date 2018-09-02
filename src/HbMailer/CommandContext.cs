using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

using HbMailer.Jobs;
using HbMailer.Jobs.Dispatcher;

namespace HbMailer {
  public class CommandContext {
    public Settings       Settings { get; set; }
    public MailJobContext MailJobCtx { get; set; }
    public MailJobManager MailJobManager { get; set; }
  }
}
