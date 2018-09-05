using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace HbMailer {
  [SetUpFixture]
  public class Global {

    [OneTimeSetUp]
    public void Setup() {
      Env.Current.Setup();
    }
  }
}
