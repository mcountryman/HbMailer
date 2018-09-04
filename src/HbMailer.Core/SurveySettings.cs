using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer {
  public class SurveySettings : ISettings {
    /// <summary>
    /// Throw an exception when settings are invalid.
    /// </summary>
    public virtual async Task Validate() { }
  }
}
