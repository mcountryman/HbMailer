using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Jobs;
using HbMailer.Jobs.Surveys;
using HbMailer.Jobs.Dispatcher;
using HbMailer.Models;

namespace HbMailer {
  public enum JobMode {
    None,
    Edit,
    Create,
  }

  public class AppContext : INotifyPropertyChanged {
    private JobModel              _job;
    private BindingList<JobModel> _jobs;
    private JobMode               _jobMode;
    private Settings              _settings;
    private bool                  _showJobFlyout;

    public JobModel Job {
      get => _job;
      set {
        _job = value;
        RaisePropertyChanged();
      }
    }

    public BindingList<JobModel> Jobs {
      get => _jobs;
      set {
        _jobs = value;
        RaisePropertyChanged();
      }
    }

    public JobMode JobMode {
      get => _jobMode;
      set {
        _jobMode = value;
        ShowJobFlyout =
          value == JobMode.Edit |
          value == JobMode.Create
        ;
        RaisePropertyChanged();
      }
    }

    public Settings Settings {
      get => _settings;
      set {
        _settings = value;
        RaisePropertyChanged();
      }
    }

    public bool ShowJobFlyout {
      get => _showJobFlyout;
      set {
        _showJobFlyout = value;
        RaisePropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public AppContext() {
      Job = new JobModel();
      Jobs = new BindingList<JobModel>();
      JobMode = JobMode.None;
      Settings = new Settings();
    }

    public void LoadJobs() {
      var jobs = MailJob.LoadAll(Settings.JobsFolder);

      Jobs.Clear();
      jobs.
        ForEach(job => Jobs.Add(new JobModel(job)))
      ;
    }

    private void RaisePropertyChanged([CallerMemberName]string propertyName = "") {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
