using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HbMailer {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {
    public AppContext Context { get; set; }

    private void OnStartup(object sender, StartupEventArgs e) {
      Context = new AppContext();
      Context.LoadJobs();
    }

    #region Singleton
    public static new App Current {
      get => Application.Current as App;
    }
    #endregion
  }
}
