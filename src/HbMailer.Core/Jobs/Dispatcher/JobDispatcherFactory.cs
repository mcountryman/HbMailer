using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer.Jobs.Dispatcher {
  public class JobDispatcherFactory {
    /// <summary>
    /// Creates E-mail service dispatcher
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public IJobDispatcher CreateDispatcher(Settings settings) {
      if (settings.EmailService is MandrillSettings) {
        return new MandrillDispatcher();
      }

      // TODO: More expressive Exception
      throw new InvalidOperationException(
        $"No valid E-mail dispatch service configuration found"  
      );
    }
  }
}
