using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer.Jobs.Impl {
  public class DataTableMapResult {
    public Dictionary<string, int> Columns { get; set; }
    public List<int> UnmappedColumns { get; set; }
  }

  public class DataTableMapper {
    /// <summary>
    /// Map columns names to column ordinals and collects all un-mapped columns.
    /// </summary>
    /// <param name="data">DataTable containg columns to be mapped</param>
    /// <param name="names">Colum names to be mapped</param>
    /// <returns></returns>
    public DataTableMapResult MapColumns(DataTable data, List<string> names, bool throw_on_unmapped = true) {
      List<string> namesCopy = new List<string>(names);
      DataTableMapResult result = new DataTableMapResult() {
        Columns = new Dictionary<string, int>(),
        UnmappedColumns = new List<int>(),
      };

      for (int i = 0; i < data.Columns.Count; i++) {
        DataColumn column = data.Columns[i];

        if (namesCopy.Contains(column.ColumnName)) {
          // Remove column name so we don't attempt to search for it again
          namesCopy.Remove(column.ColumnName);
          // Add column ordinal and name to Dictionary
          result.Columns[column.ColumnName] = column.Ordinal;

        } else if (namesCopy.Count <= 0) {
          // All column names previously found.  Add everything else to unmapped
          //  columns List and break.
          result.UnmappedColumns.AddRange(
            data.Columns
              .Cast<DataColumn>()
              .ToList()
              .GetRange(i, data.Columns.Count - i)
              .Select(x => x.Ordinal)
              .ToArray()
          );

          break;
        } else {
          // Column name not found. Add to unmapped columns List.
          result.UnmappedColumns.Add(column.Ordinal);
        }
      }

      // Check if all names were mapped
      if (namesCopy.Count != 0) {
        throw new Exception("Generic exception");
      }

      return result;
    }
  }
}
