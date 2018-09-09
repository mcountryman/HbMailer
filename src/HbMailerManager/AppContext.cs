using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Models;
using HbMailer.Helpers;
using HbMailer.ViewModels;
using HbMailer.Properties;

namespace HbMailer {
  public class AppContext {
    public JobViewModel JobViewModel { get; set; }
    public MainViewModel MainViewModel { get; set; }
    public JobListViewModel JobListViewModel { get; set; }
    public SettingsViewModel SettingsViewModel { get; set; }

    public AppContext() {
      JobViewModel = new JobViewModel(this, DialogCoordinator.Instance);
      MainViewModel = new MainViewModel(this, DialogCoordinator.Instance);
      JobListViewModel = new JobListViewModel(this, DialogCoordinator.Instance);
      SettingsViewModel = new SettingsViewModel(this, DialogCoordinator.Instance);
    }

    public void Load() {
      SettingsViewModel.Settings.Filename = GetFilename(Resources.SettingsPath);

      try {
        SettingsViewModel.Load();
      } catch {
        // Swallow this exception
      }

      try {
        if (String.IsNullOrEmpty(SettingsViewModel.Settings.JobsFilename))
          SettingsViewModel.Settings.JobsFilename = GetFilename(Resources.JobsPath);

        JobListViewModel.JobsModel.Filename = SettingsViewModel.Settings.JobsFilename;
        JobListViewModel.Load();
      } catch {
        // Swallow this exception
      }
    }

    private string GetFilename(string filename) {
      filename = Environment.ExpandEnvironmentVariables(filename);
      string directory = Path.GetDirectoryName(filename);

      if (!Directory.Exists(directory))
        Directory.CreateDirectory(directory);

      return filename;
    }
  }
}
