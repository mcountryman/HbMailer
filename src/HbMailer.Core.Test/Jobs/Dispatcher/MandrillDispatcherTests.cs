using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace HbMailer.Jobs.Dispatcher {
  [TestFixture]
  public class MandrillDispatcherTests {
    [Test]
    public async Task TestVerifySettings() {
      // Test valid api key
      await new MandrillSettings() {
        ApiKey = Env.Current.MandrillApiKey,
      }.Validate();

      // Test invalid api key
      Assert.ThrowsAsync(
        typeof(FormatException),
        async () => await new MandrillSettings() {
          ApiKey = "",
        }.Validate()
      );
    }

    [Test]
    public void TestFormatData() {
      var job = new MailJob();
      var dispatcher = new MandrillDispatcher();
      
      job.NameColumn = "emailName";
      job.EmailColumn = "emailAddress";
      job.SurveyUrlMergeField = "SURVEYURL";
      job.Recipients = new List<MailJobRecipient>() {
        new MailJobRecipient() {
          Name = "Marvin Countryman",
          Email = "me@maar.vin",
          MergeFields = new Dictionary<string, object>() {
            { "EID", "me@maar.vin" },
            { "LN", "Countryman" },
            { "NAME", "Marvin" },
            { "ADY1", "123 Fake St" },
            { "CTY", "Nowhere" },
            { "ST", "OK" },
            { "ZC", "12345" },
            { "SD", DateTime.Now },
            { "TEC1", "MCOU" },
            { "TEC2", "" },
            { "TEC3", "" },
            { "MN", "7178675309" },
          },
        },
      };

      var message = dispatcher.FormatMessage(job);

      return;
    }
  }
}
