using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace HbMailer {
  public abstract class Model {

    [XmlIgnore]
    public string Filename { get; set; }

    public void Save() {
      using (FileStream stream = File.Open(Filename, FileMode.Create))
        new XmlSerializer(GetType()).Serialize(stream, this);
    }

    public void Load() {
      try {
        Type type;
        object self;

        // Read and deserialize file
        using (FileStream stream = File.Open(Filename, FileMode.OpenOrCreate, FileAccess.Read))
          self = new XmlSerializer(GetType()).Deserialize(stream);
      } catch {

      }
    }

    public static T Load<T>(string filename) where T : Model, new() {
      var model = new T() {
        Filename = filename,
      };

      model.Load();

      return model;
    }
  }
}
