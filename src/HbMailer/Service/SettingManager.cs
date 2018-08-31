using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using NLog;

using HbMailer.Model;

namespace HbMailer.Service {
  public class SettingManager {
    private AppCtx _ctx;
    private Logger _logger;

    public SettingManager(AppCtx ctx) {
      _ctx = ctx;
      _logger = LogManager.GetLogger("SettingManager");
    }
    
    public bool TryLoad(string filename) {
      try {
        using (var stream = File.OpenRead(filename))
          _ctx.Setting = Setting.CreateSerializer().Deserialize(stream) as Setting;

        return true;
      } catch (FileNotFoundException ex) {
        _logger.Warn($"Unable to find settings file {filename}.  Creating new file with default settings.");

        _ctx.Setting = new Setting();
        return TrySave(filename);

      } catch (InvalidOperationException ex) {
        _logger.Fatal($"Unable to serialize settings file {filename}.");
      } catch (Exception ex) {
        _logger.Fatal(ex);
      }

      return false;
    }

    public bool TrySave(string filename) {
      try {
        using (var stream = File.Open(filename, FileMode.Create))
          Setting.CreateSerializer().Serialize(stream, _ctx.Setting);

        return true;
      } catch (Exception ex) {
        _logger.Fatal(ex);
        return false;
      }
    }
  }
}
