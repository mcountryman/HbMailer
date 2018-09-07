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
    private IDialogCoordinator _dialogCoordinator;

    public JobModel Job {
      get => AppContext.Job;
      set => AppContext.Job = value;
    }

    public ICommand SaveCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public JobViewModel(AppContext ctx, IDialogCoordinator dialogCoordinator) {
      AppContext = ctx;
      _dialogCoordinator = dialogCoordinator;

      SaveCommand = new RelayCommand(() => Save());
      DeleteCommand = new RelayCommand(() => Delete(), () => AppContext.JobMode == JobMode.Edit);
      CancelCommand = new RelayCommand(() => Cancel());
    }

    private void Save() {
      Job.Save();
      AppContext.JobMode = JobMode.None;
    }

    private void Delete() {
      Job.Delete(AppContext.Jobs);
      AppContext.JobMode = JobMode.None;
    }

    private void Cancel() {
      AppContext.JobMode = JobMode.None;
    }
  }
}
