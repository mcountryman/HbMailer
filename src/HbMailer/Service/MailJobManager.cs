using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace HbMailer.Service {
  public class MailJobManager {
    private MailJobCtx _ctx;

    public MailJobManager(MailJobCtx ctx) {
      _ctx = ctx;
    }

    public bool TryLoadJob(string filename) { return false; }

    public bool TryLoadJobs(string folder) {
      try {
        // Create folder if it does not exist
        if (!Directory.Exists(folder))
          Directory.CreateDirectory(folder);

        // Iterate over all files in directory and load with TryLoadJob
        foreach (var filename in Directory.GetFiles(folder))
          TryLoadJob(filename);

        return true;
      } catch (Exception ex) {
        _ctx.Logger.Fatal(ex, "Failed to load jobs");
      }

      return false;
    }
    
    public bool TryConnect() {
      try {
        _ctx.SqlConnection = new SqlConnection(_ctx.Setting.DbConnectionString);
        _ctx.SqlConnection.Open();

        return true;
      } catch (Exception ex) {
        _ctx.Logger.Fatal(ex, $"Failed to connect to database.  Check DbConnectionString in setting file.");
      }

      return false;
    }

    public bool RunJobs() {
      return false;
    }
  }
}
