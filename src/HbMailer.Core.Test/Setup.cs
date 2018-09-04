using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace HbMailer {
  [SetUpFixture]
  public class Setup {
    public string EnvironmentFile {
      get => Path.ChangeExtension(GetType().Assembly.Location, ".env");
    }

    [OneTimeSetUp]
    public void SetupEnvironment() {
      // Check if environment file exists
      if (File.Exists(EnvironmentFile)) {
        // Iterate over lines
        foreach (string assignement in File.ReadLines(EnvironmentFile)) {
          string[] parts = assignement.Split('=');

          // Just skip over invalid variables
          if (parts.Length > 1) {
            string name = parts.ElementAtOrDefault(0).Trim();
            string value = parts.ElementAtOrDefault(1).Trim();

            Environment.SetEnvironmentVariable(name, value);
          }
        }
      }
    }
  }
}
