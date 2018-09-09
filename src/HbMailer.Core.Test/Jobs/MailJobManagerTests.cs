using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using HbMailer.Jobs.Surveys;
using HbMailer.Jobs.Dispatcher;

namespace HbMailer.Jobs {
  [TestFixture]
  public class MailJobManagerTests {
    [Test]
    public void TestRun() {
      Assert.Pass("This test should only be run manually!");

      var ctx = new MailJobContext() {
        Settings = new Settings() {
          EmailService = new MandrillSettings() {
            ApiKey = Env.Current.MandrillApiKey,
          },
          SurveySettings = new SurveySquareSettings() {
            ApiKey = Env.Current.SurveySquareApiKey,
          },
          DbConnectionString = Env.Current.ConnectionString,
        },
      };
      var job = new MailJob();
      var manager = new MailJobManager(ctx);

      job.Query = @"
        SELECT
          'Marvin Countryman' AS emailName,
          'me@maar.vin' AS emailAddress,
          'me@maar.vin' AS EID,
          'Countryman' AS LN,
          '123 Fake Street' AS ADY1,
          'Nowhere' AS CTY,
          'OK' AS ST,
          '12345' AS ZC,
          GETDATE() AS SD,
          'MCOU' AS TEC1,
          '' AS TEC2,
          '' AS TEC3,
          'Marvin' AS Name,
          '7178675309' AS MN
      ";
      job.NameColumn = "emailName";
      job.EmailColumn = "emailAddress";
      job.SurveyUrlMergeField = "SURVEYURL";
      job.SurveySettings = new SurveySquareJobSettings() {
        SurveyId = "TWS1TWCK",
      };
      job.DispatcherSettings = new MandrillJobSettings() {
        Template = "testtemplate"
      };


      manager.RunJob(job);
    }
  }
}
