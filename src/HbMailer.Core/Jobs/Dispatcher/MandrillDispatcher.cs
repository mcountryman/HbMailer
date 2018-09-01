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
  /// <summary>
  /// Settings object for https://mandrillapp.com E-mail service.
  /// </summary>
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

  /// <summary>
  /// MailJob dispatcher for https://mandrillapp.com.
  /// </summary>
  public class MandrillDispatcher : IJobDispatcher {

    /// <summary>
    /// Dispatch MailJob to Mandrill service through web API.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="job">Job to dispatch</param>
    public void Dispatch(MailJobContext ctx, MailJob job) {
      MandrillSettings settings = ctx.Settings.EmailService as MandrillSettings;

      // Re-initialize API
      MandrillApi mandrill = new MandrillApi(settings.ApiKey);
      // Create task to send formatted email job
      Task<List<EmailResult>> resultsTask = mandrill.SendMessageTemplate(
        new SendMessageTemplateRequest(
          FormatMessage(job),
          job.Template
        )
      );

      // Run task
      resultsTask.RunSynchronously();
    }

    /// <summary>
    /// Format MailJob to EmailMessage object
    /// </summary>
    /// <param name="job"></param>
    /// <returns></returns>
    public EmailMessage FormatMessage(MailJob job) {
      EmailMessage message = new EmailMessage();

      // Format recipient list
      message.To = job.Recipients
        .Select(x => new EmailAddress(x.Email, x.Name))
      ;

      // Format recipient metadata
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
