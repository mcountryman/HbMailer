using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;

using HbMailer;
using HbMailer.Jobs.Dispatcher;
using HbMailerApp.Helper;

namespace HbMailerApp.ViewModels {
  public class SettingsViewModel : INotifyPropertyChanged {
    private Settings _settings;
    private SqlConnectionStringBuilder _connectionString;
    
    public string ApiKey {
      get => (_settings.EmailService as MandrillSettings).ApiKey;
      set {
        (_settings.EmailService as MandrillSettings).ApiKey = value;
        RaisePropertyChanged("ApiKey");
      }
    }

    public string Hostname {
      get => _connectionString.DataSource;
      set {
        _connectionString.DataSource = value;
        _settings.DbConnectionString = _connectionString.ToString();

        RaisePropertyChanged("Hostname");
      }
    }

    public string Database {
      get => _connectionString.InitialCatalog;
      set {
        _connectionString.InitialCatalog = value;
        _settings.DbConnectionString = _connectionString.ToString();

        RaisePropertyChanged("Database");
      }
    }

    public ICommand ValidateCommand { get; set; }

    public SettingsViewModel() : this(new Settings() {
      EmailService = new MandrillSettings()
    }) {}

    public SettingsViewModel(Settings settings) {
      _settings = settings;
      _connectionString = new SqlConnectionStringBuilder(
        _settings.DbConnectionString  
      );

      Hostname = _connectionString.DataSource;
      Database = _connectionString.InitialCatalog;

      ValidateCommand = new AsyncRelayCommand(async sender => await Validate());
    }

    private async Task Validate() {
      await _settings.Validate();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void RaisePropertyChanged(string prop)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
  }
}
