using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HbMailer.Jobs;
using HbMailer.Jobs.Surveys;
using HbMailer.Jobs.Dispatcher;

namespace HbMailer {
  public class AppContext {
    public MailJob MailJob { get; set; }
    public bool EditMode { get; set; }
  }
}
