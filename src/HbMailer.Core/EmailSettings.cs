using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer {
  /// <summary>
  /// Base E-mail settings object
  /// </summary>
  public class EmailSettings : ISettings {
    /// <summary>
    /// Throw an exception when settings are invalid.
    /// </summary>
    public virtual async Task Validate() { }
  }
}
