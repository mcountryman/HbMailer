using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace HbMailer.Jobs.Surveys.SurveySquare {
  [TestFixture]
  public class SurveySquareApiTests {
    private string _apiKey;
    private SurveySquareApi _surveySquare;

    [SetUp]
    public void SurveySquareApiTestsSetup() {
      _apiKey = Env.Current.SurveySquareApiKey;

    }

    [Test, Order(1)]
    public void TestAuthenticate() {
      // _surveySquare = new SurveySquareApi(_apiKey);
    }

    [Test]
    public void TestGetSurveys() {
      // var surveys = _surveySquare.GetSurveys();
    }

    [Test]
    public void TestGetSurveyLink() {
      // string surveyLink = _surveySquare.GetSurveyLink("", new Models.QueryStringField[] { });
    }
  }
}
