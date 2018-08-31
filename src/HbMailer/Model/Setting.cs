using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using NLog;
using NLog.Config;

namespace HbMailer.Model
{
  public class Setting
  {
    public string DbConnectionString { get; set; } = @"";

    public static XmlSerializer CreateSerializer() => new XmlSerializer(typeof(Setting));
  }
}
