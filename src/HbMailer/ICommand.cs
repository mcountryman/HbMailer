using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbMailer {
  public interface ICommand {
    string Name { get; }
    Dictionary<string, string> Arguments { get; }

    void Run(CommandContext ctx, string[] arguments);
  }
}
