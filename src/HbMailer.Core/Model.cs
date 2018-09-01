using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace HbMailer {
  public abstract class Model {

    /// <summary>
    /// Path to XML file.
    /// </summary>
    [XmlIgnore]
    public string Filename { get; private set; }

    /// <summary>
    /// Serializes object as XML and saves to file.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    /// <exception cref="PathTooLongException"/>
    /// <exception cref="DirectoryNotFoundException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="UnauthorizedAccessException"/>
    /// <exception cref="NotSupportedException"/>
    public void Save() {
      using (FileStream stream = File.Open(Filename, FileMode.Create))
        new XmlSerializer(GetType()).Serialize(stream, this);
    }

    /// <summary>
    /// Deserializes XML from file and copies properties and fields into instance.
    /// </summary>
    public void Load() {
      Type type;
      object that;

      // Read and deserialize file
      using (FileStream stream = File.Open(Filename, FileMode.OpenOrCreate, FileAccess.Read))
        that = new XmlSerializer(GetType()).Deserialize(stream);

      // Load object properties and fields into current instance
      loadFromObject(that);
    }

    private void loadFromObject(object that) {
      Type type = GetType();

      foreach (FieldInfo field in type.GetFields()) {
        if (field.IsPublic && !field.IsSpecialName) {
          field.SetValue(this, field.GetValue(that));
        }
      }

      foreach (PropertyInfo property in type.GetProperties()) {
        if (property.CanRead && property.CanWrite && !property.IsSpecialName) {
          property.SetValue(this, property.GetValue(that));
        }
      }
    }

    /// <summary>
    /// Load Model from file.
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    /// <param name="filename">Path to XML file</param>
    /// <returns></returns>
    public static T Load<T>(string filename) where T : Model, new() {
      if (String.IsNullOrEmpty(filename))
        throw new ArgumentNullException("filename");

      // Create new model
      var model = new T() {
        Filename = filename,
      };

      // Load from disk
      model.Load();
      
      return model;
    }
  }
}
