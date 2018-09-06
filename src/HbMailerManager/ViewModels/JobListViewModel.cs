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
using HbMailer.Helpers;

namespace HbMailer.ViewModels {
  public class JobListViewModel : ViewModel {
    private BindingList<MailJob> _jobs = new BindingList<MailJob>();
    private IDialogCoordinator   _dialogCoordinator;

    public BindingList<MailJob> Jobs {
      get => _jobs;
      set {
        _jobs = value;
        RaisePropertyChanged();
      }
    }

    public MailJob SelectedJob {
      get => AppContext.MailJob;
      set {
        AppContext.MailJob = value;
        RaisePropertyChanged();
      }
    }

    public ICommand NewJob { get; set; }
    public ICommand RunJob { get; set; }
    public ICommand EditJob { get; set; }
    public ICommand DeleteJob { get; set; }

    public JobListViewModel(AppContext ctx, IDialogCoordinator dialogCoordinator) {
      AppContext = ctx;
      _dialogCoordinator = dialogCoordinator;

      NewJob = new RelayCommand<object>(async (_) => await NewJobCommand());
      RunJob = new RelayCommand<object>(async (_) => await RunJobCommand(), x => SelectedJob != null);
      EditJob = new RelayCommand<object>(async (_) => await EditJobCommand(), x => SelectedJob != null);
      DeleteJob = new RelayCommand<object>(async (_) => await DeleteJobCommand(), x => SelectedJob != null);

    }

    private async Task NewJobCommand() {
      var name = await _dialogCoordinator.ShowInputAsync(
        this,
        "Job name",
        "Enter name of job");
      var job = new MailJob() {
        Filename = Path.Combine(Environment.CurrentDirectory, "Jobs", name + ".xml"),
      };

      Jobs.Add(job);
      SelectedJob = job;
      SelectedJob.Save();
    }

    private async Task RunJobCommand() { }

    private async Task EditJobCommand() {
      AppContext.EditMode = true;
    }

    private async Task DeleteJobCommand() {
      if (SelectedJob == null) {
        await _dialogCoordinator.ShowMessageAsync(
          this,
          ":(",
          "You must select a job to delete!",
          MessageDialogStyle.Affirmative);
      } else {
        string confirmation = await _dialogCoordinator.ShowInputAsync(
          this,
          "Confirm",
          "Type name of job you wish to delete to confirm");

        if (SelectedJob.Name == confirmation) {
          Jobs.Remove(SelectedJob);

          await _dialogCoordinator.ShowMessageAsync(
            this,
            "Success!",
            $"Successfully deleted job \"{SelectedJob.Name}\"!");

          SelectedJob = null;
        }
      }
    }
  }
}
