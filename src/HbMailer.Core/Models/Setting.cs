using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using NLog;
using NLog.Config;

namespace HbMailer.Models
{
  public class Setting
  {
    public string MandrillApiKey { get; set; } = @"";
    public string DbConnectionString { get; set; } = @"";

    public static XmlSerializer CreateSerializer() => new XmlSerializer(typeof(Setting));
  }
}
