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

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Jobs;
using HbMailer.ViewModels;

namespace HbMailer.Views {
  /// <summary>
  /// Interaction logic for JobListView.xaml
  /// </summary>
  public partial class JobListView : UserControl {
    public static readonly DependencyProperty AppContextProperty = DependencyProperty.Register(
      "AppContext",
      typeof(AppContext),
      typeof(JobListView),
      new PropertyMetadata()
    );

    public AppContext AppContext {
      get => GetValue(AppContextProperty) as AppContext;
      set => SetValue(AppContextProperty, value);
    }

    public JobListView() {
      InitializeComponent();
      DataContext = new JobListViewModel(
        AppContext,
        DialogCoordinator.Instance
      );
    }
  }
}
