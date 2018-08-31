using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace HbMailer.Model {
  public class MailJobMergeField {
    [XmlAttribute]
    public string FieldName;
    [XmlAttribute]
    public string QueryName;
  }

  public class MailJob {
    public string Query { get; set; }
    public string Template { get; set; }
    
    [XmlArray]
    public List<MailJobMergeField> MergeFields { get; set; }
    
    public static XmlSerializer CreateSerializer() => new XmlSerializer(typeof(MailJob));
  }
}
