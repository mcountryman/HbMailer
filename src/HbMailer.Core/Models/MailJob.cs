using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace HbMailer.Models {
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

    #region Disk Interface

    #endregion
  }
}
