using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer.Jobs.Dispatcher {
  public class JobDispatcherFactory {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public IJobDispatcher CreateDispatcher(Settings settings) {
      if (settings.EmailService is MandrillSettings) {
        return new MandrillDispatcher();
      }

      // TODO: More expressive Exception
      throw new Exception("");
    }
  }
}
