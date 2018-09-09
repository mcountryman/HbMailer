using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Models;
using HbMailer.Helpers;

namespace HbMailer.ViewModels {
  public class MainViewModel : ViewModel {
    private JobMode _jobMode;
    private bool    _showJobFlyout;
    private bool    _showSettingsFlyout;

    public JobMode JobMode {
      get => _jobMode;
      set {
        _jobMode = value;
        ShowJobFlyout =
          value == JobMode.Edit ||
          value == JobMode.Create
        ;
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

    public bool ShowSettingsFlyout {
      get => _showSettingsFlyout;
      set {
        _showSettingsFlyout = value;
        RaisePropertyChanged();
      }
    }

    public MainViewModel(
      AppContext ctx,
      IDialogCoordinator dialogCoordinator
    ) : base(ctx, dialogCoordinator) {

    }
  }
}
