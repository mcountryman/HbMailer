using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data.SqlClient;

using HbMailer;
using HbMailer.Models;

namespace HbMailer.Jobs {
  public class MailJobManager {
    private MailJobContext _ctx;

    public MailJobManager(MailJobContext ctx) {
      _ctx = ctx;
    }

    
  }
}
