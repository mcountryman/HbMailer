using System;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HbMailer.Jobs {
  [XmlType("job")]
  public class MailJob : Model {
    [XmlArray("fields")]
    public List<MailJobField> Fields { get; set; }
    [XmlElement("query")]
    public string Query { get; set; }
    [XmlElement("template")]
    public string Template { get; set; }
    [XmlElement("recipient_name_column")]
    public string NameColumn { get; set; } = "RecipientName";
    [XmlElement("recipient_email_column")]
    public string EmailColumn { get; set; } = "RecipientEmail";
    [XmlIgnore]
    public List<MailJobRecipient> Recipients { get; set; }
      = new List<MailJobRecipient>();

    public static MailJob Load(string filename)
      => Load<MailJob>(filename);
  }
}
