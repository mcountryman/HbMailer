using System;
using System.Xml.Serialization;

namespace HbMailer.Jobs.Surveys.SurveySquare.Models {
  [XmlType("Survey")]
  public class Survey {
    [XmlElement("Id")]
    public int Id { get; set; }
    [XmlElement("Name")]
    public string Name { get; set; }
  }
}
