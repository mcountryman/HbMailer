using System;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using NLog;

namespace HbMailer.Jobs {
  /// <summary>
  /// MailJob contains all E-mail campaign job data.
  /// </summary>
  [XmlType("job")]
  public class MailJob : Model {
    /// <summary>
    /// Get Name of job from Model.Filename
    /// </summary>
    [XmlIgnore]
    public string Name {
      get => Path.GetFileNameWithoutExtension(Filename);
    }

    /// <summary>
    /// Get NLog logger
    /// </summary>
    [XmlIgnore]
    public Logger Logger {
      get => LogManager.GetLogger(Name);
    }

    /// <summary>
    /// SQL query string used to build recipient list.
    /// </summary>
    [XmlElement("query")]
    public string Query { get; set; }

    /// <summary>
    /// Email campaign service template id.
    /// </summary>
    [XmlElement("template")]
    public string Template { get; set; }

    /// <summary>
    /// Recipient name SQL query result column.
    /// </summary>
    [XmlElement("recipient_name_column")]
    public string NameColumn { get; set; } = "RecipientName";

    /// <summary>
    /// Recipient email SQL query result column.
    /// </summary>
    [XmlElement("recipient_email_column")]
    public string EmailColumn { get; set; } = "RecipientEmail";

    /// <summary>
    /// Generated list of recipients.
    /// </summary>
    [XmlIgnore]
    public List<MailJobRecipient> Recipients { get; set; }
      = new List<MailJobRecipient>();

    /// <summary>
    /// Load new instance from disk.
    /// </summary>
    /// <param name="filename">Path to XML file.</param>
    /// <returns></returns>
    public static MailJob Load(string filename)
      => Load<MailJob>(filename);
  }
}
