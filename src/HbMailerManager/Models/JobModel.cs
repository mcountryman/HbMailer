using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Jobs;
using HbMailer.Helpers;

namespace HbMailer.Models {
  public class JobModel : Notifier {
    private MailJob _job;

    public string Name {
      get => _job.Name;
      set {
        _job.Name = value;
        RaisePropertyChanged();
      }
    }

    public string Filename {
      get => _job.Filename;
      set {
        _job.Filename = value;
        RaisePropertyChanged();
      }
    }

    public JobModel() {
      _job = new MailJob() {
        Filename = Path.Combine(
          Environment.CurrentDirectory,
          "Jobs",
          "Unknown.xml"
        ),
      };
    }
    public JobModel(MailJob job) {
      _job = job;
    }

    public void Save() => _job.Save();
    public void Save(object sender, IDialogCoordinator dialog) { }

    public void Load() => _job.Load();
    public void Load(object sender, IDialogCoordinator dialog) { }

    public void Delete(IList<JobModel> container) {
      container.Remove(this);

      if (File.Exists(_job.Filename))
        File.Delete(_job.Filename);

      _job = new MailJob();
    }

    public async Task DeleteAsync(IList<JobModel> container, object sender, IDialogCoordinator dialog) {
      var confirmation = await dialog.ShowInputAsync(
        sender,
        "Are you sure you want to do this?",
        "Type the job name to confirm.."
      );

      if (String.IsNullOrEmpty(confirmation))
        return;

      if (confirmation == _job.Name)
        Delete(container);

      await dialog.ShowMessageAsync(
        sender,
        "Winner!",
        $"Successfully deleted job \"{confirmation}\""
      );
    }
  }
}
