using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Jobs;
using HbMailer.Helpers;

namespace HbMailer.ViewModels {
  public class JobViewModel : ViewModel {
    private IDialogCoordinator _dialogCoordinator;

    public MailJob Job {
      get => AppContext.MailJob;
    }

    public bool EditMode {
      get => AppContext.EditMode;
    }

    public JobViewModel(AppContext ctx, IDialogCoordinator dialogCoordinator) {
      AppContext = ctx;
      _dialogCoordinator = dialogCoordinator;
    }
  }
}
