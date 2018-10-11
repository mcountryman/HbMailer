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
    private JobsModel _jobs = new JobsModel();
    private JobModel _selectedJob;

    public BindingList<JobModel> Jobs {
      get => _jobs.Jobs;
    }

    public JobsModel JobsModel {
      get => _jobs;
    }

    public JobModel SelectedJob {
      get => _selectedJob;
      set {
        _selectedJob = value;
        RaisePropertyChanged();
      }
    }

    public ICommand NewCommand { get; set; }
    public ICommand RunCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public JobListViewModel(
      AppContext ctx,
      IDialogCoordinator dialogCoordinator
    ) : base(ctx, dialogCoordinator) {

      NewCommand = new RelayCommand(() => New());
      RunCommand = new RelayCommand(
        () => Run(SelectedJob),
        () =>  SelectedJob != null
      );
      EditCommand = new RelayCommand(
        () => Edit(SelectedJob),
        () => SelectedJob != null
      );
      DeleteCommand = new RelayCommand(
        async () => await Delete(SelectedJob),
        () => SelectedJob != null
      );
    }

    public void New() {
      var job = new JobModel();

      Ctx.JobViewModel.Job = job;
      Ctx.MainViewModel.JobMode = JobMode.Create;
    }

    public void Run(JobModel job) { }

    public void Edit(JobModel job) {
      Ctx.JobViewModel.Job = job;
      Ctx.MainViewModel.JobMode = JobMode.Edit;
    }

    public async Task Delete(JobModel job) {
      var result = await DialogCoordinator.ShowMessageAsync(
        this,
        "Are you sure?",
        "Do you really want to delete this job?",
        MessageDialogStyle.AffirmativeAndNegative
      );

      if (result != MessageDialogResult.Affirmative)
        return;

      Jobs.Remove(job);

      if (SelectedJob == job)
        SelectedJob = null;

      await DialogCoordinator.ShowMessageAsync(
        this,
        "Winner!",
        $"Successfully deleted job!"
      );

      _jobs.Save();
    }

    public void Load() {
      _jobs.Load();
    }

    public void Save() {
      _jobs.Save();
    }
  }
}
