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

using HbMailer.ViewModels;

namespace HbMailer {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow {
    public MainWindow() {
      InitializeComponent();
      DataContext = App.Current.Context.MainViewModel;
    }

    private void OnSettingsClicked(object sender, RoutedEventArgs e) {
      (DataContext as MainViewModel).ShowSettingsFlyout =
        !(DataContext as MainViewModel).ShowSettingsFlyout;
    }
  }
}
