using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using NLog;

using HbMailer;
using HbMailer.Model;

namespace HbMailer.Service {
  public class MailJobCtx {
    public Logger Logger { get; set; }
    public Setting Setting { get; set; }
    public SqlConnection SqlConnection { get; set; }
  }
}
