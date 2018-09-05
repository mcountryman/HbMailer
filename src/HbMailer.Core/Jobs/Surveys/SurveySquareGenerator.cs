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

    public override async Task Validate() {
      await Task.FromResult(new SurveySquareApi(ApiKey));
    }
  }

  public class SurveySquareJobSettings : SurveyJobSettings {
    public string SurveyId { get; set; }
    public bool ValidateParameters { get; set; }
  }

  public class SurveySquareGeneratorCtx {
    public MailJob MailJob { get; set; }
    public MailJobRecipient MailJobRecipient { get; set; }
    public SurveySquareApi SurveyApi { get; set; }
    public SurveySquareSettings SurveySettings { get; set; }
    public SurveySquareJobSettings SurveyJobSettings { get; set; }

    public static SurveySquareGeneratorCtx Create(MailJobContext ctx, MailJob job, MailJobRecipient recipient) {
      if (!(ctx.Settings.SurveySettings is SurveySquareSettings))
        throw new InvalidOperationException("Settings missing SurveySquare configuration");
      if (!(job.SurveySettings is SurveySquareJobSettings))
        throw new InvalidOperationException("Job missing SurveySquare configuration");

      var jobSettings = job.SurveySettings as SurveySquareJobSettings;
      var settings = ctx.Settings.SurveySettings as SurveySquareSettings;
      var api = new SurveySquareApi(settings.ApiKey);

      return new SurveySquareGeneratorCtx() {
        MailJob = job,
        MailJobRecipient = recipient,
        SurveyApi = api,
        SurveySettings = settings,
        SurveyJobSettings = jobSettings,
      };
    }
  }

  public class SurveySquareGenerator : ISurveyGenerator {
    public string Name {
      get { return "SurveySquare"; }
    }

    public string GenerateLink(MailJobContext mailJobCtx, MailJob job, MailJobRecipient recipient) {
      var ctx = SurveySquareGeneratorCtx.Create(mailJobCtx, job, recipient);

      return ctx.SurveyApi.GetSurveyLink(
        ctx.SurveyJobSettings.SurveyId,
        GenerateQueryStringFields(ctx)
      );
    }

    public QueryStringField[] GenerateQueryStringFields(SurveySquareGeneratorCtx ctx) {
      var fields = ctx.SurveyApi.GetQueryStringFields(
        ctx.SurveyJobSettings.SurveyId
      ).ToList();
      var recipient = ctx.MailJobRecipient;

      foreach (var field in fields) {
        try {
          var mergeField = recipient.MergeFields.First(
            x => x.Key == field.Key
          );

          field.Value = mergeField.Value.ToString();
        } catch(InvalidOperationException ex) {
          if (ctx.SurveyJobSettings.ValidateParameters)
            throw new InvalidOperationException(
              $"Unable to resolve SurveySquare parameter '{field.Label}'",
              ex
            );
        } 
      }

      return fields.ToArray();
    }
  }
}
