using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer.Jobs.Surveys {
  public class SurveyGeneratorFactory {
    /// <summary>
    /// Create an ISurveyGenerator instance based from the Settings provided in
    ///   the MailJobContext.
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public ISurveyGenerator CreateSurveyGenerator(MailJobContext ctx) {
      if (ctx.Settings.SurveySettings is SurveySquareSettings) {
        return new SurveySquareGenerator();
      }

      throw new InvalidOperationException(
        "No valid survey generator configuration found"
      );
    }
  }
}
