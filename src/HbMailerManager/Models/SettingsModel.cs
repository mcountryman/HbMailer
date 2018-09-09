using System;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using HbMailer.Helpers;

namespace HbMailer.Models {
  public class SettingsModel : PropertyNotifier {
    private Settings _settings;
    private SqlConnectionStringBuilder _connectionString;

    public string Filename {
      get => _settings.Filename;
      set {
        _settings.Filename = value;
        RaisePropertyChanged();
      }
    }

    public string JobsFilename {
      get => _settings.JobsFilename;
      set {
        _settings.JobsFilename = value;
        RaisePropertyChanged();
      }
    }

    public string Server {
      get => _connectionString.DataSource;
      set {
        _connectionString.DataSource = value;
        _settings.DbConnectionString = _connectionString.ToString();
        RaisePropertyChanged();
      }
    }

    public string Database {
      get => _connectionString.InitialCatalog;
      set {
        _connectionString.InitialCatalog = value;
        _settings.DbConnectionString = _connectionString.ToString();
        RaisePropertyChanged();
      }
    }

    public string Username {
      get => _connectionString.UserID;
      set {
        _connectionString.UserID = value;
        _settings.DbConnectionString = _connectionString.ToString();
        RaisePropertyChanged();
      }
    }

    public string Password {
      get => _connectionString.Password;
      set {
        _connectionString.Password = value;
        _settings.DbConnectionString = _connectionString.ToString();
        RaisePropertyChanged();
      }
    }

    public bool WindowsAuthentication {
      get => _connectionString.IntegratedSecurity;
      set {
        _connectionString.IntegratedSecurity = value;
        _settings.DbConnectionString = _connectionString.ToString();
        RaisePropertyChanged();
      }
    }

    public string MandrillApiKey {
      get => _settings.EmailService.ApiKey;
      set {
        _settings.EmailService.ApiKey = value;
        RaisePropertyChanged();
      }
    }

    public string SurveySquareApiKey {
      get => _settings.SurveySettings.ApiKey;
      set {
        _settings.SurveySettings.ApiKey = value;
        RaisePropertyChanged();
      }
    }

    public SettingsModel() {
      _settings = new Settings();
      _connectionString = new SqlConnectionStringBuilder(
        _settings.DbConnectionString
      );
    }

    public void Load() {
      _settings.Load();
      _connectionString = new SqlConnectionStringBuilder(
        _settings.DbConnectionString
      );

      RaisePropertyChangingAll();
    }

    public void Save() {
      _settings.Save();
    }
  }
}
