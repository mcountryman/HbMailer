using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Concurrent;

using NLog;

namespace HbMailer {
  public abstract class Model {
    private Logger _logger = LogManager.GetLogger("Model");
    private FileSystemWatcher _watcher;

    /// <summary>
    /// Path to XML file.
    /// </summary>
    [XmlIgnore]
    public string Filename { get; set; }

    /// <summary>
    /// Enable file watching
    /// </summary>
    [XmlIgnore]
    public bool Watch {
      get {
        if (_watcher != null)
          return _watcher.EnableRaisingEvents;

        return false;
      }
      set {
        if (String.IsNullOrEmpty(Filename))
          return;

        if (_watcher == null && value) {
          _watcher = new FileSystemWatcher();
          _watcher.Path = Path.GetDirectoryName(Filename);
          _watcher.Changed += (sender, args) => {
            if (args.FullPath == Filename)
              Load();
          };
          _watcher.Created += (sender, args) => {
            if (args.FullPath == Filename)
              Load();
          };
          _watcher.Renamed += (sender, args) => {
            if (args.OldFullPath == Filename) {
              Filename = args.FullPath;
              _watcher.Path = Path.GetDirectoryName(Filename);
            }
          };
        }

        if (_watcher != null)
          _watcher.EnableRaisingEvents = value;
      }
    }

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
      using (FileStream stream = File.Open(Filename, FileMode.Create, FileAccess.Write))
        new XmlSerializer(GetType()).Serialize(stream, this);

      _logger.Debug($"Saved {Filename}");
    }

    /// <summary>
    /// Deserializes XML from file and copies properties and fields into instance.
    /// </summary>
    public void Load() {
      object that;

      // Check if file exists
      if (!File.Exists(Filename))
        throw new FileNotFoundException();

      // Read and deserialize file
      using (FileStream stream = File.OpenRead(Filename))
        that = new XmlSerializer(GetType()).Deserialize(stream);

      // Load object properties and fields into current instance
      loadFromObject(that);

      _logger.Debug($"Loaded {Filename}");
    }

    private void loadFromObject(object that) {
      Type type = GetType();

      foreach (FieldInfo field in type.GetFields()) {
        if (
          field.IsPublic &&
          !field.IsSpecialName &&
          field.GetCustomAttribute(typeof(XmlIgnoreAttribute)) == null
        ) {
          field.SetValue(this, field.GetValue(that));
        }
      }

      foreach (PropertyInfo property in type.GetProperties()) {
        if (
          property.CanRead &&
          property.CanWrite &&
          !property.IsSpecialName &&
          property.GetCustomAttribute(typeof(XmlIgnoreAttribute)) == null
        ) {
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
    protected static T Load<T>(string filename) where T : Model, new() {
      if (String.IsNullOrEmpty(filename))
        throw new ArgumentNullException("filename");

      // Create new model
      var model = new T() {
        Filename = Path.GetFullPath(filename),
      };

      // Load from disk
      model.Load();
      
      return model;
    }

    /// <summary>
    /// Load Model from file.  If not exists create new file and Model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filename"></param>
    /// <returns></returns>
    protected static T LoadSafe<T>(string filename) where T : Model, new() {
      T instance;

      try {
        instance = Load<T>(filename);
      } catch (FileNotFoundException) {
        instance = new T();
        instance.Filename = Path.GetFullPath(filename);
        instance.Save();
      }

      return instance;
    }
    
    protected static List<T> LoadAll<T>(
      string folder,
      string pattern = "*.xml",
      bool createDirectoryIfNotExists = true
    ) where T : Model, new() {
      var result = new ConcurrentBag<T>();

      if (!Directory.Exists(folder)) {
        if (createDirectoryIfNotExists)
          Directory.CreateDirectory(folder);

        return result.ToList();
      }

      Parallel.ForEach(
        Directory.GetFiles(folder, pattern),
        filename => result.Add(LoadSafe<T>(filename))
      );

      return result.ToList();
    }
  }
}
