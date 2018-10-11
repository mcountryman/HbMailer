using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace HbMailer {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {
    private object contextLock = new object();
    public AppContext Context { get; set; }

    private void OnStartup(object sender, StartupEventArgs e) {
      lock (contextLock) {
        Context = new AppContext();
        Context.Load();
      }
    }

    #region Singleton
    public static new App Current {
      get => Application.Current as App;
    }
    #endregion
  }
}
