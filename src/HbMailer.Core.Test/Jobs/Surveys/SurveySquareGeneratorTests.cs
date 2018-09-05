using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace HbMailer.Jobs.Surveys {
  [TestFixture]
  public class SurveySquareGeneratorTests {

    [SetUp]
    public void SurveySquareGeneratorTestsSetup() {
      
    }

    [Test]
    public void TestGenerateLink() {
      var ctx = new MailJobContext() {
        Settings = new Settings() {
          SurveySettings = new SurveySquareSettings() {
            ApiKey = Env.Current.SurveySquareApiKey,
          }
        }
      };

      var job = new MailJob() {
        SurveySettings = new SurveySquareJobSettings() {
          SurveyId = "TWS1TWCK",
        },
      };

      var recipient = new MailJobRecipient() {
        Name = "Marvin Countryman",
        Email = "***REMOVED***",
        MergeFields = new Dictionary<string, object>() {

        },
      };

      var generator = new SurveySquareGenerator();

      var link = generator.GenerateLink(ctx, job, recipient);

      return;
    }
  }
}
