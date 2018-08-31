using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using NLog;

using HbMailer.Models;

namespace HbMailer {
  public class SettingManager {
    private CoreCtx _ctx;

    public SettingManager(CoreCtx ctx) {
      _ctx = ctx;
    }
    
    public Setting Load(string filename) {
      try {
        using (var stream = File.OpenRead(filename))
          return Setting.CreateSerializer().Deserialize(stream) as Setting;
      } catch (FileNotFoundException ex) {
        return new Setting();
      }
    }

    public void Save(string filename, Setting setting) {
      using (var stream = File.Open(filename, FileMode.Create))
        Setting.CreateSerializer().Serialize(stream, setting);
    }
  }
}
