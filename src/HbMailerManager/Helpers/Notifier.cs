﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace HbMailer.Helpers {
  public abstract class Notifier : INotifyPropertyChanged, INotifyPropertyChanging {
    public event PropertyChangedEventHandler PropertyChanged;
    public event PropertyChangingEventHandler PropertyChanging;

    protected void RaisePropertyChanged([CallerMemberName]string name = "") {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    protected void RaisePropertyChanging([CallerMemberName]string name = "") {
      PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
    }
  }
}
