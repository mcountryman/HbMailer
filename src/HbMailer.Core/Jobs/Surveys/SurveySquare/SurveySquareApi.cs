using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HbMailer.Jobs.Surveys.SurveySquare.Models;

namespace HbMailer.Jobs.Surveys.SurveySquare {
  public class SurveySquareApi {
    private string oAuthToken;
    private SurveySquareSoapApi soapApi;

    public SurveySquareApi(string apiKey) {
      soapApi = new SurveySquareSoapApi();
      oAuthToken = soapApi.getOuthToken(apiKey);
    }


    public string GetSurveyLink(string surveyId, QueryStringField[] queryStringFields) {
      return soapApi.GenerateSurveyLinkXml(oAuthToken, surveyId, queryStringFields);
    }

    public QueryStringField[] GetQueryStringFields(string surveyId) {
      return soapApi.GetQueryStringFieldsxml(oAuthToken, surveyId);
    }
  }
}
