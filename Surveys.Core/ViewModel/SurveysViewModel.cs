using System.Collections.ObjectModel;
using System.Windows.Input;
using Surveys.Core.Model;
using Xamarin.Forms;

namespace Surveys.Core.ViewModel
{
    public class SurveysViewModel : ViewModelBase
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

        public SurveysViewModel()
        {
            NewSurveyCommand = new Command(NewSurveyCommandExecute);
            Surveys = new ObservableCollection<Survey>();
            MessagingCenter.Subscribe<SurveyDetailsViewModel, Survey>(this, Messages.NewSurveyComplete, (sender, args) => 
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
