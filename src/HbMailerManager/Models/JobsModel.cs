using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;

using HbMailer.Helpers;

namespace HbMailer.Models {
  public class JobsModel : PropertyNotifier {
    private string _filename;

    public BindingList<JobModel> Jobs { get; }

    public string Filename {
      get => _filename;
      set {
        _filename = value;
        RaisePropertyChanged();
      }
    }

    public JobsModel() {
      Jobs = new BindingList<JobModel>();
    }

    public void Load() {
      var jobs = new List<JobModel>();

      if (!File.Exists(Filename))
        throw new FileNotFoundException();

      using (var stream = File.OpenRead(Filename))
        jobs = new XmlSerializer(typeof(List<JobModel>)).Deserialize(stream)
          as List<JobModel>;

      Jobs.Clear();
      jobs.ForEach(x => Jobs.Add(x));
    }

    public void Save() {
      using (var stream = File.Open(Filename, FileMode.Create, FileAccess.Write))
        new XmlSerializer(typeof(List<JobModel>)).Serialize(
          stream,
          Jobs.ToList()
        );
    }
  }
}
