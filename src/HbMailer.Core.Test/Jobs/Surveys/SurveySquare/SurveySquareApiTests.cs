using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using HbMailer.Jobs.Surveys.SurveySquare.Models;

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
      _surveySquare = new SurveySquareApi(_apiKey);
    }

    [Test]
    public void TestGetSurveyQueryStringFields() {
      var queryStringFields = _surveySquare.GetQueryStringFields("TWS1TWCK");

      // Only validate that we get fields back rather than length as this will
      //  change over time.
    }

    [Test]
    public void TestGetSurveyLink() {
      string surveyLink = _surveySquare.GetSurveyLink("TWS1TWCK", new QueryStringField[] {
        new QueryStringField() { Key = "EID", Label = "Email Address", Value = "mcountryman@hbmcclure.com", },
        new QueryStringField() { Key = "LN", Label = "Last Name", Value = "Countryman", },
        new QueryStringField() { Key = "ADY1", Label = "Address", Value = "554 N Chestnut St", },
        new QueryStringField() { Key = "CTY", Label = "City", Value = "Palmyra", },
        new QueryStringField() { Key = "ST", Label = "State", Value = "PA", },
        new QueryStringField() { Key = "ZC", Label = "Zip", Value = "17078", },
        new QueryStringField() { Key = "SD", Label = "Date", Value = DateTime.Now.ToShortDateString(), },
        new QueryStringField() { Key = "TEC1", Label = "Tech1", Value = "MCOU", },
        new QueryStringField() { Key = "TEC2", Label = "Tech2", Value = "", },
        new QueryStringField() { Key = "TEC3", Label = "Tech3", Value = "", },
        new QueryStringField() { Key = "Name", Label = "Name", Value = "Marvin", },
        new QueryStringField() { Key = "MN", Label = "Mobile Number", Value = "7178675309", },
      });

      Utility.ValidateUrl(surveyLink);
    }
  }
}
