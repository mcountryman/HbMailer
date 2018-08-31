using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;

namespace HbMailer.Service {
  public class MailJobDispatcher {
    private MailJobCtx _ctx;

    public MailJobDispatcher(MailJobCtx ctx) {
      _ctx = ctx;
    }

    public bool Dispatch() {
      return false;
    }

    public bool TryInitialize() {
      try {
        var api = new MandrillApi(_ctx.Setting.MandrillApiKey);

        api.Ping();

      } catch (Exception ex) {
        _ctx.Logger.Fatal(ex, "Unable to connect to Mandrill api. Check MandrillApiKey setting.");
      }

      return false;
    }
  }
}
