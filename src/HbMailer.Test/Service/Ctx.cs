using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using HbMailer.Service;

namespace HbMailer.Test.Service {
  public class Ctx {
    public AppCtx AppCtx { get; } = new AppCtx();

    #region Singleton
    private static Ctx current;
    private static object currentLock = new object();
    public static Ctx Current {
      get {
        lock (currentLock) {
          if (current == null)
            current = new Ctx();
          return current;
        }
      }
    }
    #endregion
  }
}
