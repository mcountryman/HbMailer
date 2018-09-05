using System;
using System.Collections.Generic;

namespace HbMailer.Jobs {
  public class MailJobRecipient {
    /// <summary>
    /// Recipient display name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Recipient E-mail address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Additional fields used when rendering HTML template.
    /// </summary>
    public Dictionary<string, object> MergeFields { get; set; }
      = new Dictionary<string, object>();
  }
}
