using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Jobs;
using HbMailer.Models;
using HbMailer.Helpers;

namespace HbMailer.ViewModels {
  public class JobListViewModel : ViewModel {
    private BindingList<JobModel> _jobs = new BindingList<JobModel>();
    private IDialogCoordinator   _dialogCoordinator;

    public BindingList<JobModel> Jobs {
      get => AppContext.Jobs;
    }

    public JobModel SelectedJob {
      get => AppContext.Job;
      set {
        AppContext.Job = value;
        RaisePropertyChanged();
      }
    }
    
    public ICommand NewCommand { get; set; }
    public ICommand RunCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public JobListViewModel(AppContext ctx, IDialogCoordinator dialogCoordinator) {
      AppContext = ctx;
      _dialogCoordinator = dialogCoordinator;

      NewCommand = new RelayCommand(async () => await New());
      RunCommand = new RelayCommand(async () => await Run(), () => SelectedJob != null);
      EditCommand = new RelayCommand(async () => await Edit(), () => SelectedJob != null);
      DeleteCommand = new RelayCommand(async () => await Delete(), () => SelectedJob != null);
    }

    private async Task New() {
      var job = new JobModel();

      Jobs.Add(job);
      SelectedJob = job;

      AppContext.JobMode = JobMode.Create;
    }

    private async Task Run() { }

    private async Task Edit() {
      AppContext.Job = SelectedJob;
      AppContext.JobMode = JobMode.Edit;
    }

    private async Task Delete() {
      await SelectedJob.DeleteAsync(AppContext.Jobs, this, _dialogCoordinator);
    }
  }
}
