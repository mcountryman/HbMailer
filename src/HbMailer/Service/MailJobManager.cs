using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data.SqlClient;

using HbMailer;
using HbMailer.Model;

namespace HbMailer.Service {
  public class MailJobManager {
    private MailJobCtx _ctx;

    public MailJobManager(MailJobCtx ctx) {
      _ctx = ctx;
    }

    public bool TryLoadJob(string filename) {
      try {
        using (var stream = File.OpenRead(filename))
          _ctx.Jobs.Enqueue(MailJob.CreateSerializer().Deserialize(stream) as MailJob);

        return true;
      } catch (Exception ex) {
        _ctx.Logger.Error(ex, $"Failed to load job {filename}");
      }

      return false;
    }

    public void SaveJob(string filename, MailJob job) {
      using (var stream = File.Open(filename, FileMode.Create))
        MailJob.CreateSerializer().Serialize(stream, job);
    }

    public bool TryLoadJobs(string folder) {
      try {
        // Create folder if it does not exist
        if (!Directory.Exists(folder))
          Directory.CreateDirectory(folder);

        // Iterate over all files in directory and load with TryLoadJob
        Parallel.ForEach(
          Directory.GetFiles(folder),
          (filename) => TryLoadJob(filename)
        );

        return true;
      } catch (Exception ex) {
        _ctx.Logger.Fatal(ex, "Failed to load jobs");
      }

      return false;
    }
    
    public bool TryInitialize() {
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
