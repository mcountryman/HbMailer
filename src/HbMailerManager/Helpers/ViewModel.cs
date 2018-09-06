using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace HbMailer.Helpers {
  public abstract class ViewModel : INotifyPropertyChanged {
    private AppContext _appContext;

    public event PropertyChangedEventHandler PropertyChanged;

    protected AppContext AppContext {
      get => _appContext != null ? _appContext : new AppContext();
      set => _appContext = value;
    }

    protected void RaisePropertyChanged([CallerMemberName]string name = "") {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
