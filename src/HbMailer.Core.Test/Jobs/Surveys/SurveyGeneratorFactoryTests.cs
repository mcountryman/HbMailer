using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace HbMailer.Jobs.Surveys {
  [TestFixture]
  public class SurveyGeneratorFactoryTests {

    [Test]
    public void TestSurveySquareCreation() {
      var settings = new Settings() {
        SurveySettings = new SurveySquareSettings() {
          ApiKey = "",
        }
      };
      var factory = new SurveyGeneratorFactory();
      var surveyGenerator = factory.CreateSurveyGenerator(new MailJobContext() {
        Settings = settings,
      });

      Assert.IsInstanceOf(typeof(SurveySquareGenerator), surveyGenerator);
    }
  }
}
