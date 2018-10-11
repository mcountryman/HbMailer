using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Jobs;
using HbMailer.Models;
using HbMailer.Helpers;

namespace HbMailer.ViewModels {
  public class JobViewModel : ViewModel {
    private JobModel _job;

    public JobModel Job {
      get => _job;
      set {
        _job = value;
        RaisePropertyChanged();
      }
    }

    public ICommand SaveCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public JobViewModel(
      AppContext ctx,
      IDialogCoordinator dialogCoordinator
    ) : base(ctx, dialogCoordinator) {
      SaveCommand = new RelayCommand(() => Save());
      DeleteCommand = new RelayCommand(
        async () => await Delete(),
        () => Ctx.MainViewModel.JobMode == JobMode.Edit
      );
      CancelCommand = new RelayCommand(() => Cancel());
    }

    private void Save() {
      if (Ctx.MainViewModel.JobMode == JobMode.Create) {
        Ctx.JobListViewModel.Jobs.Add(Job);
        Ctx.JobListViewModel.SelectedJob = Job;
      }

      Ctx.JobListViewModel.Save();
      Ctx.MainViewModel.JobMode = JobMode.None;
    }

    private async Task Delete() {
      await Ctx.JobListViewModel.Delete(Job);
      Ctx.MainViewModel.JobMode = JobMode.None;
    }

    private void Cancel() {
      Ctx.MainViewModel.JobMode = JobMode.None;
    }
  }
}
