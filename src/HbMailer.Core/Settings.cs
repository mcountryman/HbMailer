﻿using System;
using System.Xml.Serialization;
using System.Data.SqlClient;

using NLog;
using NLog.Config;

using HbMailer.Jobs.Dispatcher;

namespace HbMailer {
  public class Settings : Model {
    [XmlElement("MandrillSettings", typeof(MandrillSettings))]
    public EmailSettings EmailService;
    [XmlElement]
    public string DbConnectionString { get; set; } = @"";

    /// <summary>
    /// Throw an exception when settings are invalid.
    /// </summary>
    public void Validate() {
      using (SqlConnection connection = new SqlConnection(DbConnectionString))
        connection.Open();

      EmailService.Validate();
    }

    /// <summary>
    /// Load new instance from disk.
    /// </summary>
    /// <param name="filename">Path to XML file.</param>
    /// <returns></returns>
    public static Settings Load(string filename)
      => Load<Settings>(filename);
  }
}