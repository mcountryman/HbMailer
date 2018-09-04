using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HbMailer.Jobs.Surveys.SurveySquare;
using HbMailer.Jobs.Surveys.SurveySquare.Models;

namespace HbMailer.Jobs.Surveys {
  public class SurveySquareSettings : SurveySettings {
    public string ApiKey { get; set; }

    public async Task Validate() {
      new SurveySquareApi(ApiKey);
    }
  }

  public class SurveySquareGenerator : ISurveyGenerator {
    public string GenerateLink(MailJobContext ctx, MailJob job) {
      if (!(ctx.Settings.SurveySettings is SurveySquareSettings))
        throw new InvalidOperationException("Unable to generate Survey Square link without Survey Square settings");

      var settings = ctx.Settings.SurveySettings as SurveySquareSettings;
      var api = new SurveySquareApi(settings.ApiKey);

      return api.GetSurveyLink("", new QueryStringField[] { });
    }
  }
}
