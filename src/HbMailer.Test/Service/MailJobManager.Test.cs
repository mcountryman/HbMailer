using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using HbMailer.Model;
using HbMailer.Service;

namespace HbMailer.Test.Service {
  [TestFixture]
  public class MailJobManager {

    [Test]
    public void LoadJob() {
      Assert.IsTrue(Ctx.Current.AppCtx.MailJobManager.TryLoadJob("jobs/test.xml"));
    }

    [Test]
    public void SaveJob() {
      Ctx.Current.AppCtx.MailJobManager.SaveJob(@"C:\Development\HbMailer\bin\Debug\jobs\text.xml", new MailJob() {
        
        Query = @"
SELECT TOP 1
  *
FROM [wo]
        ",
      });
    }
  }
}
