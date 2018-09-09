using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Models;
using HbMailer.Helpers;

namespace HbMailer.ViewModels {
  public class SettingsViewModel : ViewModel {
    private SettingsModel _settings;

    public SettingsModel Settings {
      get => _settings;
      set {
        _settings = value;
        RaisePropertyChanged();
      }
    }

    public ICommand SaveCommand { get; set; }

    public SettingsViewModel(
      AppContext ctx,
      IDialogCoordinator dialogCoordinator
    ) : base(ctx, dialogCoordinator) {
      _settings = new SettingsModel();

      SaveCommand = new RelayCommand(async () => await Save());
    }

    public void Load() {
      _settings.Load();
    }

    public async Task Save() {
      try {
        _settings.Save();
        Ctx.MainViewModel.ShowSettingsFlyout = false;
      } catch (Exception ex) {
        await DialogCoordinator.ShowMessageAsync(
          this,
          ":(",
          ex.Message
        );
      }
    }
  }
}
