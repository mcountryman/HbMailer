using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer {
  public class Env {
    public string Filename {
      get { return Path.ChangeExtension(GetType().Assembly.Location, ".env"); }
    }

    public string MandrillApiKey {
      get { return Environment.GetEnvironmentVariable("HBMAILER_MANDRILL_API_KEY"); }
    }

    public string SurveySquareApiKey {
      get { return Environment.GetEnvironmentVariable("HBMAILER_SURVEY_SQUARE_API_KEY"); }
    }

    public void Setup() {
      // Check if environment file exists
      if (File.Exists(Filename)) {
        // Iterate over lines
        foreach (string assignement in File.ReadLines(Filename)) {
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

    #region Singleton
    private static Env current;
    private static object currentLock = new object();
    public static Env Current {
      get {
        lock (currentLock) {
          if (current == null)
            current = new Env();

          return current;
        }
      }
    }
    #endregion
  }
}
