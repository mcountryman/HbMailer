using System;
using System.Data;
using System.Data.SqlClient;

namespace HbMailer.Jobs {
  /// <summary>
  /// Contains reusable objects used in MailJob processes.
  /// </summary>
  public class MailJobContext {
    public Settings Settings { get; set; }
  }
}
