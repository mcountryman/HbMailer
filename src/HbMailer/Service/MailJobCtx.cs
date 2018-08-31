using System;
using System.Data.SqlClient;
using System.Collections.Concurrent;

using NLog;

using HbMailer;
using HbMailer.Model;

namespace HbMailer.Service {
  public class MailJobCtx {
    private AppCtx _ctx;

    public Setting Setting { get => _ctx.Setting; }
    public MailJobManager MailJobManager { get => _ctx.MailJobManager; }
    public MailJobDispatcher MailJobDispatcher { get => _ctx.MailJobDispatcher; }

    public Logger Logger { get; set; }
    public SqlConnection SqlConnection { get; set; }

    public ConcurrentQueue<MailJob> Jobs { get; set; } = new ConcurrentQueue<MailJob>();

    public MailJobCtx(AppCtx ctx) {
      _ctx = ctx;
    }
  }
}
