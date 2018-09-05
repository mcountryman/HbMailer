using System;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using NLog;

using HbMailer.Jobs.Surveys;
using HbMailer.Jobs.Dispatcher;

namespace HbMailer.Jobs {
  /// <summary>
  /// MailJob contains all E-mail campaign job data.
  /// </summary>
  [XmlType("Job")]
  public class MailJob : Model {
    /// <summary>
    /// Get Name of job from Model.Filename
    /// </summary>
    [XmlIgnore]
    public string Name {
      get { return Path.GetFileNameWithoutExtension(Filename); }
    }

    /// <summary>
    /// Get NLog logger
    /// </summary>
    [XmlIgnore]
    public Logger Logger {
      get { return LogManager.GetLogger(Name); }
    }

    [XmlElement("SurveySquare", typeof(SurveySquareJobSettings))]
    public SurveyJobSettings SurveySettings { get; set; }
    
    [XmlElement("Mandrill", typeof(MandrillJobSettings))]
    public DispatcherJobSettings DispatcherSettings { get; set; }

    /// <summary>
    /// SQL query string used to build recipient list.
    /// </summary>
    [XmlElement("Query")]
    public string Query { get; set; }

    /// <summary>
    /// Recipient name SQL query result column.
    /// </summary>
    [XmlElement("RecipientNameColumn")]
    public string NameColumn { get; set; } = "RecipientName";

    /// <summary>
    /// Recipient email SQL query result column.
    /// </summary>
    [XmlElement("RecipientEmailColumn")]
    public string EmailColumn { get; set; } = "RecipientEmail";

    /// <summary>
    /// Merge field key to use when generating survey url.
    /// </summary>
    [XmlElement("SurveyUrlMergeField")]
    public string SurveyUrlMergeField { get; set; } = "SurveyUrl";

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
