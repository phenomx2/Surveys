using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Surveys.Core.ViewModel
{
    public class Data : ViewModelBase
    {
        private ObservableCollection<Survey> _surveys;
        private Survey _selectedSurvey;

        public ICommand NewSurveyCommand { get; set; }

        public ObservableCollection<Survey> Surveys
        {
            get => _surveys;
            set
            {
                if (_surveys == value) return;
                _surveys = value;
                OnPropertychanged();
            }
        }

        public Survey SelectedSurvey
        {
            get => _selectedSurvey;
            set
            {
                if (_selectedSurvey == value) return;
                _selectedSurvey = value;
                OnPropertychanged();
            }
        }

        public Data()
        {
            NewSurveyCommand = new Command(NewSurveyCommandExecute);
            Surveys = new ObservableCollection<Survey>();
            MessagingCenter.Subscribe<ContentPage, Survey>(this, Messages.NewSurveyMessage, (sender, args) => 
            {
                Surveys.Add(args);
            });
        }

        private void NewSurveyCommandExecute()
        {
            MessagingCenter.Send(this,Messages.NewSurvey);
        }
    }
}
