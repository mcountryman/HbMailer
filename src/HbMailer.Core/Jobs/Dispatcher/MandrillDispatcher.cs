using System;
using System.Xml.Serialization;
using System.Linq;
using System.Dynamic;
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
    [XmlElement("ApiKey")]
    public string ApiKey { get; set; }

    public override async Task Validate() {
      try {
        await new MandrillApi(ApiKey).Ping();
      } catch (MandrillException ex) {
        throw new FormatException($"Invalid Mandrill API key", ex);
      }
    }
  }

  [XmlType("MandrillSettings")]
  public class MandrillJobSettings : DispatcherJobSettings {
    public string Template { get; set; }
  }

  /// <summary>
  /// MailJob dispatcher for https://mandrillapp.com.
  /// </summary>
  public class MandrillDispatcher : IJobDispatcher {
    public string Name { get; } = "Mandrill";

    /// <summary>
    /// Dispatch MailJob to Mandrill service through web API.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="job">Job to dispatch</param>
    public void Dispatch(MailJobContext ctx, MailJob job) {
      if (!(ctx.Settings.EmailService is MandrillSettings))
        throw new InvalidOperationException("Settings missing Mandrill configuration");
      if (!(job.DispatcherSettings is MandrillJobSettings))
        throw new InvalidOperationException("Job missing Mandrill configuration");

      MandrillSettings settings = ctx.Settings.EmailService as MandrillSettings;
      MandrillJobSettings jobSettings = job.DispatcherSettings as MandrillJobSettings;

      // Re-initialize API
      MandrillApi mandrill = new MandrillApi(settings.ApiKey);
      // Create task to send formatted email job
      List<EmailResult> results = mandrill.SendMessageTemplate(
        new SendMessageTemplateRequest(
          FormatMessage(job),
          jobSettings.Template
        )
      ).Result;

      return;
    }

    /// <summary>
    /// Format MailJob to EmailMessage object
    /// </summary>
    /// <param name="job"></param>
    /// <returns></returns>
    public EmailMessage FormatMessage(MailJob job) {
      var emailMessage = new EmailMessage();

      emailMessage.To = job.Recipients
        .Select(x => new EmailAddress(x.Email, x.Name));

      job.Recipients
        .ForEach(x =>
          x.MergeFields
            .ToList()
            .ForEach(y => 
              emailMessage.AddRecipientVariable(x.Email, y.Key, y.Value)
            )
        )
      ;

      return emailMessage;
    }
  }
}
