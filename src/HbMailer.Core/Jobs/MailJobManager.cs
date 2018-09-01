using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data.SqlClient;

using HbMailer;
using HbMailer.Jobs.Impl;
using HbMailer.Jobs.Dispatcher;

namespace HbMailer.Jobs {
  public class MailJobManager {
    private MailJobContext _ctx;
    private IJobDispatcher _dispatcher;
    private RecipientResolver _recipientResolver;

    public MailJobManager(MailJobContext ctx) {
      _ctx = ctx;
    }

    public void RunJob(string filename) {
      MailJob job = MailJob.Load(filename);

      _recipientResolver.Resolve(_ctx, job);
      _dispatcher.Dispatch(_ctx, job);
    }
  }
}
