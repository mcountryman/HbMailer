using System;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HbMailerApp.Helper {
  //http://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030

  public class RelayCommand : ICommand {
    #region Fields

    readonly Action<object> _execute;
    readonly Predicate<object> _canExecute;

    #endregion // Fields

    #region Constructors

    public RelayCommand(Action<object> execute)
    : this(execute, null) {
    }

    public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
      if (execute == null)
        throw new ArgumentNullException("execute");

      _execute = execute;
      _canExecute = canExecute;
    }
    #endregion // Constructors

    #region ICommand Members

    [DebuggerStepThrough]
    public bool CanExecute(object parameter) {
      return _canExecute == null ? true : _canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter) {
      _execute(parameter);
    }

    #endregion // ICommand Members
  }

  public class AsyncRelayCommand : ICommand {
    private readonly Func<object, Task> execute;
    private readonly Func<object, bool> canExecute;

    private long isExecuting;

    public AsyncRelayCommand(Func<object, Task> execute, Func<object, bool> canExecute = null) {
      this.execute = execute;
      this.canExecute = canExecute ?? (o => true);
    }

    public event EventHandler CanExecuteChanged {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void RaiseCanExecuteChanged() {
      CommandManager.InvalidateRequerySuggested();
    }

    public bool CanExecute(object parameter) {
      if (Interlocked.Read(ref isExecuting) != 0)
        return false;

      return canExecute(parameter);
    }

    public async void Execute(object parameter) {
      Interlocked.Exchange(ref isExecuting, 1);
      RaiseCanExecuteChanged();

      try {
        await execute(parameter);
      } finally {
        Interlocked.Exchange(ref isExecuting, 0);
        RaiseCanExecuteChanged();
      }
    }
  }
}
