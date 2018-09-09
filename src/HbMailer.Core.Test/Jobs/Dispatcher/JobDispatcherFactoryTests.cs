using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace HbMailer.Jobs.Dispatcher {
  [TestFixture]
  public class JobDispatcherFactoryTests {

    [Test]
    public void TestJobDispatcherCreation() {
      var factory = new JobDispatcherFactory();
      var settings = new Settings() {
        EmailService = new MandrillSettings(),
      };
      var jobDispatcher = factory.CreateDispatcher(settings);

      Assert.IsInstanceOf(typeof(MandrillDispatcher), jobDispatcher);
    }

    [Test]
    public void TestExceptionOnInvalidSettings() {
      var factory = new JobDispatcherFactory();
      var settings = new Settings() {
        EmailService = new MandrillSettings(),
      };

      Assert.Throws(
        typeof(InvalidOperationException),
        () => factory.CreateDispatcher(settings)
      );
    }

  }
}
