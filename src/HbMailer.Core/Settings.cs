using System;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Threading.Tasks;

using NLog;
using NLog.Config;

using HbMailer.Jobs.Surveys;
using HbMailer.Jobs.Dispatcher;

namespace HbMailer {
  public class Settings : Model {
    [XmlElement("MandrillSettings", typeof(MandrillSettings))]
    public EmailSettings EmailService = new MandrillSettings() {
      ApiKey = "MANDRILL_API_KEY",
    };

    [XmlElement("SurveySquareSettings", typeof(SurveySquareSettings))]
    public SurveySettings SurveySettings = new SurveySettings();

    [XmlElement("JobsFolder")]
    public string JobsFolder { get; set; } = Path.Combine(
      Environment.CurrentDirectory,
      "Jobs"
    );

    [XmlElement]
    public string DbConnectionString { get; set; } = @"Data Source=localhost; Integrated Security=SSPI;";

    /// <summary>
    /// Throw an exception when settings are invalid.
    /// </summary>
    public async Task Validate() {
      using (SqlConnection connection = new SqlConnection(DbConnectionString))
        await connection.OpenAsync();

      await EmailService.Validate();
    }

    /// <summary>
    /// Load new instance from disk.
    /// </summary>
    /// <param name="filename">Path to XML file.</param>
    /// <returns></returns>
    public static Settings Load(string filename)
      => Load<Settings>(filename);

    public static Settings LoadSafe(string filename)
      => LoadSafe<Settings>(filename);
  }
}
