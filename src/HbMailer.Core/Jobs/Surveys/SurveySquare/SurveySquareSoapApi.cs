using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

using HbMailer.Jobs.Surveys.SurveySquare.Models;

namespace HbMailer.Jobs.Surveys.SurveySquare {
  [WebServiceBinding(Name ="SSDSSoap", Namespace = "http://www.surveysquare.com")]
  public class SurveySquareSoapApi : SoapHttpClientProtocol {
    public SurveySquareSoapApi() {
      Url = "https://surveysquare.com/api/ssds.asmx";
    }

    [SoapDocumentMethod("https://surveysquare.com/GetOAuthToken")]
    public string GetOAuthToken(string apiKey) {
      return (
        Invoke(
          "getOuthToken",
          new object[] { apiKey }
        )[0]
      ) as string;
    }

    [SoapDocumentMethod("https://www.surveysquare.com/GetSurveysXml")]
    public Survey[] GetSurveysXml(string oAuthToken) {
      return (
        Invoke(
          "GetSurveysXml",
          new object[] { oAuthToken }
        )[0]
      ) as Survey[];
    }

    [SoapDocumentMethod("https://www.surveysquare.com/GenerateSurveyLink")]
    public string GenerateSurveyLink(
      string oAuthToken, 
      string surveyId,
      QueryStringField[] queryStringFields
    ) {
      return (
        Invoke(
          "GenerateSurveyLink",
          new object[] {
            oAuthToken,
            surveyId,
            queryStringFields,
          }
        )[0]
      ) as string;
    }
  }
}
