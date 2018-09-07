using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace HbMailer.Helpers {
  public abstract class ViewModel : Notifier {
    public AppContext AppContext { get; set; }
  }
}
