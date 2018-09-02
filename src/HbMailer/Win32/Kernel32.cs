using System;
using System.Runtime.InteropServices;

namespace HbMailer.Win32 {
  internal class Kernel32 {
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AllocConsole();
  }
}
