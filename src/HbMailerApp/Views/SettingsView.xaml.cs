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

using HbMailerApp.ViewModels;

namespace HbMailerApp.Views {
  /// <summary>
  /// Interaction logic for SettingsView.xaml
  /// </summary>
  public partial class SettingsView : UserControl {
    public SettingsView() : this(new SettingsViewModel()) { }
    public SettingsView(SettingsViewModel viewModel) {
      InitializeComponent();
      DataContext = viewModel;
    }
  }
}
