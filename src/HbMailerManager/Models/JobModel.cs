using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MahApps.Metro.Controls.Dialogs;

using HbMailer.Jobs;
using HbMailer.Jobs.Surveys;
using HbMailer.Jobs.Dispatcher;
using HbMailer.Helpers;

namespace HbMailer.Models {
  public class JobModel : PropertyNotifier {
    private MailJob _job;

    public string Name {
      get => _job.Name;
      set {
        _job.Name = value;
        RaisePropertyChanged();
      }
    }

    public string Query {
      get => _job.Query;
      set {
        _job.Query = value;
        RaisePropertyChanged();
      }
    }

    public string RecipientNameColumn {
      get => _job.NameColumn;
      set {
        _job.NameColumn = value;
        RaisePropertyChanged();
      }
    }

    public string RecipientEmailColumn {
      get => _job.EmailColumn;
      set {
        _job.EmailColumn = value;
        RaisePropertyChanged();
      }
    }

    public string Template {
      get => (_job.DispatcherSettings as MandrillJobSettings).Template;
      set {
        (_job.DispatcherSettings as MandrillJobSettings).Template = value;
        RaisePropertyChanged();
      }
    }

    public string SurveyId {
      get => (_job.SurveySettings as SurveySquareJobSettings).SurveyId;
      set {
        (_job.SurveySettings as SurveySquareJobSettings).SurveyId = value;
        RaisePropertyChanged();
      }
    }

    public bool ValidateSurveyMergeFields {
      get => (_job.SurveySettings as SurveySquareJobSettings).ValidateParameters;
      set {
        (_job.SurveySettings as SurveySquareJobSettings).ValidateParameters = value;
        RaisePropertyChanged();
      }
    }

    public string SurveyUrlMergeField {
      get => _job.SurveyUrlMergeField;
      set {
        _job.SurveyUrlMergeField = value;
        RaisePropertyChanged();
      }
    }

    public JobModel() {
      _job = new MailJob() {
        SurveySettings = new SurveySquareJobSettings() { },
        DispatcherSettings = new MandrillJobSettings() { },
        Name = "",
      };
    }

    public JobModel(MailJob job) {
      _job = job;
    }
  }
}
