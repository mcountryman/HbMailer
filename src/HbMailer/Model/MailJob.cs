using System;
using System.Xml.Serialization;

namespace HbMailer.Model {
  public class MailJob {
    public string Query { get; set; }
    
    public static XmlSerializer CreateSerializer() => new XmlSerializer(typeof(MailJob));
  }
}
