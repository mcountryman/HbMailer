﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer.Jobs.Dispatcher {
  public interface IJobDispatcher {
    /// <summary>
    /// Get name of E-mail service;
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Dispatch MailJob to E-mail service.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="job"></param>
    void Dispatch(MailJobContext ctx, MailJob job);
  }
}
