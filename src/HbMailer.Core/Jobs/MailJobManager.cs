using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data.SqlClient;

using HbMailer;
using HbMailer.Jobs.Impl;
using HbMailer.Jobs.Surveys;
using HbMailer.Jobs.Dispatcher;

namespace HbMailer.Jobs {
  /// <summary>
  /// Handles all MailJob processes from cradle to grave.
  /// </summary>
  public class MailJobManager {
    private MailJobContext _ctx;
    private IJobDispatcher _dispatcher;
    private ISurveyGenerator _surveyGenerator;
    private RecipientResolver _recipientResolver;

    /// <summary>
    /// Construct and configures MailJobManager
    /// </summary>
    /// <param name="ctx">Utilized for Settings object down the line when a database
    /// connection needs to be made and a job needs sent to an E-mail campaign service.</param>
    public MailJobManager(MailJobContext ctx) {
      _ctx = ctx;
      _dispatcher = new JobDispatcherFactory().CreateDispatcher(ctx.Settings);
      _recipientResolver = new RecipientResolver();
    }

    /// <summary>
    /// Runs MailJob from XML file
    /// </summary>
    /// <param name="filename"></param>
    public void RunJob(string filename) {
      RunJob(MailJob.Load(filename));
    }

    /// <summary>
    /// Resolves recipient email, name, and metadata from query then dispatches
    /// email with recipient list to configured email service.
    /// </summary>
    /// <param name="job"></param>
    public void RunJob(MailJob job) {
      try {
        _recipientResolver.Resolve(_ctx, job);
        job.Logger.Debug($"Resolved {job.Recipients.Count} recipients.");

        // Generate survey urls and insert as merge field
        foreach (var recipient in job.Recipients) { 
          recipient.MergeFields[job.SurveyUrlMergeField] = _surveyGenerator.GenerateLink(
            _ctx,
            job,
            recipient
          );

          job.Logger.Debug($"Generated survey url for recipient '{recipient.Email}'");
        }
        job.Logger.Debug($"Generated survey urls for recipients");
        _dispatcher.Dispatch(_ctx, job);
        job.Logger.Debug($"Dispatched email campaign to '{_dispatcher.Name}'");
        job.Logger.Info("Job successfull");
      } catch (Exception ex) {
        job.Logger.Error(ex, "Job failed");
      }
    }
  }
}
