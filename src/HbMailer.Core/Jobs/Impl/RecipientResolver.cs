using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HbMailer.Jobs.Impl {
  /// <summary>
  /// Execute query and format results as MailJobRecipient List.
  /// </summary>
  public class RecipientResolver {
    public List<MailJobRecipient> Resolve(
      MailJobContext ctx,
      MailJob        job
    ) {
      using (SqlConnection connection = new SqlConnection(ctx.Settings.DbConnectionString)) {
        connection.Open();

        var command = new SqlCommand(job.Query, connection);
        var recipients = new List<MailJobRecipient>();

        // TODO: Populate command parameters

        // Format resulting data
        using (SqlDataReader reader = command.ExecuteReader()) {
          // Load query results into DataTable
          DataTable data = new DataTable();
          data.Load(reader);

          // Resolve recipients from DataTable query results
          return FormatData(data, job);
        }
      }
    }

    /// <summary>
    /// Resolve MailJobRecipient from DataTable input.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="job"></param>
    /// <returns></returns>
    public List<MailJobRecipient> FormatData(DataTable data, MailJob job) {
      var recipients = new List<MailJobRecipient>();
      
      // Find name, email, and merge field column ordinals
      DataTableMapResult mapResult = new DataTableMapper().MapColumns(
        data,
        new List<string>() {
          job.NameColumn,
          job.EmailColumn,
        }
      );

      // Iterate over rows
      foreach (DataRow row in data.Rows) {
        MailJobRecipient recipient = new MailJobRecipient();

        recipient.Name = row[mapResult.Columns[job.NameColumn]].ToString();
        recipient.Email = row[mapResult.Columns[job.EmailColumn]].ToString();

        foreach (int mergeOrdinal in mapResult.UnmappedColumns) {
          recipient.MergeFields.Add(
            data.Columns[mergeOrdinal].ColumnName,
            row[mergeOrdinal]
          );
        }

        recipients.Add(recipient);
      }

      job.Recipients = recipients;
      return recipients;
    }
  }
}
