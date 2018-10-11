using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;

using MahApps.Metro.Controls.Dialogs;

namespace HbMailer.Helpers {
  public abstract class ViewModel : PropertyNotifier {
    protected AppContext Ctx { get; set; }
    protected IDialogCoordinator DialogCoordinator { get; set; }

    public ViewModel(AppContext ctx, IDialogCoordinator dialogCoordinator) {
      Ctx = ctx;
      DialogCoordinator = dialogCoordinator;
    }
  }
}
