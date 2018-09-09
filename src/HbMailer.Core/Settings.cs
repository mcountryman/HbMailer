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
    public MandrillSettings EmailService = new MandrillSettings() {
      ApiKey = "MANDRILL_API_KEY",
    };
    
    public SurveySquareSettings SurveySettings = new SurveySquareSettings();

    [XmlElement("JobsFile")]
    public string JobsFilename { get; set; }

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
