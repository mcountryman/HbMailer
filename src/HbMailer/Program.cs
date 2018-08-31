using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HbMailer {
  class Program {
    static void Run() {

    }

    static void Main() {
      Run();

      #if DEBUG
      Console.Write("Press any key to continue...");
      Console.ReadKey();
      #endif
    }
  }
}
