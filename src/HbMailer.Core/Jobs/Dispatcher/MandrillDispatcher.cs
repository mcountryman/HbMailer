using System;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using Mandrill.Utilities;

namespace HbMailer.Jobs.Dispatcher {
  public class MandrillSettings : EmailSettings {
    public string ApiKey { get; set; }

    public override void Validate() {
      try {
        new MandrillApi(ApiKey).Ping().RunSynchronously();
      } catch (MandrillException ex) {
        throw new FormatException($"Invalid Mandrill API key", ex);
      }
    }
  }

  public class MandrillDispatcher : IJobDispatcher {
    public void Dispatch(MailJobContext ctx, MailJob job) {
      MandrillSettings settings = ctx.Settings.EmailService as MandrillSettings;
      MandrillApi mandrill = new MandrillApi(settings.ApiKey);
      Task<List<EmailResult>> resultsTask = mandrill.SendMessageTemplate(
        new SendMessageTemplateRequest(
          FormatMessage(job),
          job.Template
        )
      );

      resultsTask.RunSynchronously();
    }

    public EmailMessage FormatMessage(MailJob job) {
      EmailMessage message = new EmailMessage();

      message.To = job.Recipients
        .Select(x => new EmailAddress(x.Email, x.Name))
      ;

      message.RecipientMetadata = job.Recipients
        .Select(x => new RcptMetadata() {
          Rcpt = x.Email,
          Values = x.MergeFields,
        })
      ;

      return message;
    }
  }
}
