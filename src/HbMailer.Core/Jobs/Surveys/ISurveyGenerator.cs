using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer.Jobs.Surveys {
  public interface ISurveyGenerator {
    string GenerateLink(MailJobContext ctx, MailJob job, MailJobRecipient recipient);
  }
}
