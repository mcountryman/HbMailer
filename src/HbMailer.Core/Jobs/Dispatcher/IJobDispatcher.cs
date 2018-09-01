using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer.Jobs.Dispatcher {
  public interface IJobDispatcher {
    void Dispatch(MailJobContext ctx, MailJob job);
  }
}
