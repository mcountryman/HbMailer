using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace HbMailer {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow, INotifyPropertyChanged {
    private AppContext _appContext;

    public AppContext AppContext {
      get => _appContext;
      set {
        _appContext = value;
        RaisePropertyChanged("AppContext");
      }
    }

    public MainWindow() {
      AppContext = new AppContext();
      DataContext = this;
      InitializeComponent();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged(string name) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
