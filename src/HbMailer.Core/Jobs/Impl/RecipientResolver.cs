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
      // Create SQL connection
      using (SqlConnection connection = new SqlConnection(ctx.Settings.DbConnectionString)) {
        connection.Open();

        var command = new SqlCommand(job.Query, connection);
        var recipients = new List<MailJobRecipient>();

        // TODO: Populate command parameters
        //  Something like this maybe..?
        //  -|  foreach param {GlobalCommandParameterRegistry}:
        //  -|    command.Parameters.Add(new SqlParameter(param.Name, param.Value));

        // Format query results into MailJobRecipient List
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
    /// Resolve MailJobRecipient from DataTable input.  Assigns MailJob.Recipients
    /// field to results of this method.
    /// </summary>
    /// <param name="data">DataTable input</param>
    /// <param name="job">MailJob input</param>
    /// <returns>List of MailJobRecipient</returns>
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

        // Resolve recipient name from mapped DataTable ordinal
        //  row[                 <-- Index DataRow array
        //    mapResult.Columns[ <-- Attempt to find column ordinal from ...
        //      job.NameColumn   <-- Use name SQL column defined in MailJob XML file
        //  ]].ToString()        <-- Now we have the cell we are looking for. :D
        recipient.Name = row[mapResult.Columns[job.NameColumn]].ToString();
        // Resolve recipient email from mapped DataTable ordinal
        recipient.Email = row[mapResult.Columns[job.EmailColumn]].ToString();

        // Now we are going to iterate over all other results from our `query` and
        //  assume that these will be merge fields.
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
