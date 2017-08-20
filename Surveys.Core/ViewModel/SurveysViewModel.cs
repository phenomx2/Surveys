using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.Model;
using Surveys.Core.Views;
using Xamarin.Forms;

namespace Surveys.Core.ViewModel
{
    public class SurveysViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationServiceService; 

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
                OnPropertyChanged();
            }
        }

        public Survey SelectedSurvey
        {
            get => _selectedSurvey;
            set
            {
                if (_selectedSurvey == value) return;
                _selectedSurvey = value;
                OnPropertyChanged();
            }
        }

        public SurveysViewModel(INavigationService navigationService)
        {
            _navigationServiceService = navigationService;
            NewSurveyCommand = new DelegateCommand(NewSurveyCommandExecute);
            Surveys = new ObservableCollection<Survey>();
        }

        private async void NewSurveyCommandExecute()
        {
            await _navigationServiceService.NavigateAsync(nameof(SurveyDetail));
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("NewSurvey"))
            {
                Surveys.Add(parameters["NewSurvey"] as Survey);
            }
        }
    }
}
