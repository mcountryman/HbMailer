using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using NLog;
using NLog.Config;

using HbMailer.Jobs.Dispatcher;

namespace HbMailer {
  public class Settings : Model {
    [XmlElement("MandrillSettings", typeof(MandrillSettings))]
    public EmailSettings EmailService;
    [XmlElement]
    public string DbConnectionString { get; set; } = @"";

    public void Validate() { }

    public static Settings Load(string filename)
      => Load<Settings>(filename);
  }
}
