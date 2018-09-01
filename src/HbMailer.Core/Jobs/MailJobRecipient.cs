using System;
using System.Collections.Generic;

namespace HbMailer.Jobs {
  public class MailJobRecipient {
    public string Name { get; set; }
    public string Email { get; set; }

    public Dictionary<string, object> MergeFields { get; }
      = new Dictionary<string, object>();
  }
}
