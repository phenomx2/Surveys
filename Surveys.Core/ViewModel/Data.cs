using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Surveys.Core.ViewModel
{
    public class Data : ViewModelBase
    {
        private ObservableCollection<Survey> _surveys;

        public ObservableCollection<Survey> Surveys
        {
            get { return _surveys; }
            set
            {
                if (_surveys == value) return;
                _surveys = value;
                OnPropertychanged();
            }
        }

        private Survey _survey;

        public Survey SelectedSurvey
        {
            get { return _survey; }
            set { _survey = value; }
        }

        public Data()
        {
            Surveys = new ObservableCollection<Survey>();
            MessagingCenter.Subscribe<ContentPage, Survey>(this, Messages.NewSurveyMessage, (sender, args) => 
            {
                Surveys.Add(args);
            });
        }
    }
}
