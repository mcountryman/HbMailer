using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HbMailer.Jobs;
using HbMailer.Helpers;

namespace HbMailer.ViewModels {
  public class AppViewModel : ViewModel {
    private MailJob _job;
    private MailJob[] _jobs;

    public MailJob Job {
      get => _job;
      set {
        _job = value;
        RaisePropertyChanged();
      }
    }
  }
}
