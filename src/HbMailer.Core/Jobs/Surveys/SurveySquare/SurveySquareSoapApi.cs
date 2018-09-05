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
  [WebServiceBinding(Name = "SSDSSoap", Namespace = "http://www.SurveySquare.com/")]
  public class SurveySquareSoapApi : SoapHttpClientProtocol {
    public SurveySquareSoapApi() {
      Url = "http://www.surveysquare.com/API/SSDS.asmx";
    }

    [SoapDocumentMethod(
      "http://www.SurveySquare.com/getOuthToken",
      Use = SoapBindingUse.Literal,
      ParameterStyle = SoapParameterStyle.Wrapped,
      RequestNamespace = "http://www.SurveySquare.com/",
      ResponseNamespace = "http://www.SurveySquare.com/"
    )]
    public string getOuthToken(string APIKey) {
      return (
        Invoke(
          "getOuthToken",
          new object[] { APIKey }
        )[0]
      ) as string;
    }

    [SoapDocumentMethod(
      "http://www.SurveySquare.com/GenerateSurveyLinkXml",
      Use = SoapBindingUse.Literal,
      ParameterStyle = SoapParameterStyle.Wrapped,
      RequestNamespace = "http://www.SurveySquare.com/",
      ResponseNamespace = "http://www.SurveySquare.com/"
    )]
    public string GenerateSurveyLinkXml(
      string OuthToken, 
      string SurveyID,
      QueryStringField[] QueryStringFields
    ) {
      return (
        Invoke(
          "GenerateSurveyLinkXml",
          new object[] {
            OuthToken,
            SurveyID,
            QueryStringFields,
          }
        )[0]
      ) as string;
    }

    [SoapDocumentMethod(
      "http://www.SurveySquare.com/GetQueryStringFieldsxml",
      Use = SoapBindingUse.Literal,
      ParameterStyle = SoapParameterStyle.Wrapped,
      RequestNamespace = "http://www.SurveySquare.com/",
      ResponseNamespace = "http://www.SurveySquare.com/"
    )]
    public QueryStringField[] GetQueryStringFieldsxml(
      string OuthToken,
      string SurveyID
    ) {
      return (
        Invoke(
          "GetQueryStringFieldsxml",
          new object[] {
            OuthToken,
            SurveyID,
          }
        )[0]
      ) as QueryStringField[];
    }
  }
}
