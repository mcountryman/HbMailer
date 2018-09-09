using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Microsoft.Win32;

using HbMailer.Helpers;

namespace HbMailer.Controls {
  public partial class SaveFileBox : UserControl, INotifyPropertyChanged {
    public static readonly DependencyProperty FilenameProperty = DependencyProperty.Register(
      "Filename",
      typeof(string),
      typeof(SaveFileBox),
      new FrameworkPropertyMetadata() {
        BindsTwoWayByDefault = true,
      }
    );

    public string Filename {
      get => GetValue(FilenameProperty) as string;
      set => SetValue(FilenameProperty, value);
    }

    public ICommand BrowseCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public SaveFileBox() {
      BrowseCommand = new RelayCommand(() => Browse());
      InitializeComponent();
    }

    private void Browse() {
      var dialog = new SaveFileDialog();

      dialog.Filter = "XML File|*.xml||*.*";
      dialog.AddExtension = true;
      dialog.ShowDialog();

      if (dialog.FileName != "") {
        Filename = dialog.FileName;
      }
    }

    private void RaisePropertyChanged([CallerMemberName]string name = "") {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
