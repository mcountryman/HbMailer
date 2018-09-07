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
    public AppContext AppContext { get; set; }

    public MainWindow() {
      InitializeComponent();
      AppContext = App.Current.Context;
      DataContext = this;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged(string name) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
