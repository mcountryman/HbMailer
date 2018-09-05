using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace HbMailer.Jobs.Dispatcher {
  [TestFixture]
  public class MandrillDispatcherTests {
    [Test]
    public async Task TestVerifySettings() {
      // Test valid api key
      await new MandrillSettings() {
        ApiKey = Env.Current.MandrillApiKey,
      }.Validate();

      // Test invalid api key
      Assert.ThrowsAsync(
        typeof(FormatException),
        async () => await new MandrillSettings() {
          ApiKey = "",
        }.Validate()
      );
    }
  }
}
