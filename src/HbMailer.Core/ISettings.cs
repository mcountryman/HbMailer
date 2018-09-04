using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer {
  public interface ISettings {
    /// <summary>
    /// Throw an exception when settings are invalid.
    /// </summary>
    Task Validate();
  }
}
