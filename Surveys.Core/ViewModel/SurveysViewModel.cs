using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.Model;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Views;

namespace Surveys.Core.ViewModel
{
    public class SurveysViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationServiceService;
        private readonly ILocalDbService _localDbService;

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

        public SurveysViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationServiceService = navigationService;
            _localDbService = localDbService;
            NewSurveyCommand = new DelegateCommand(NewSurveyCommandExecute);
            Surveys = new ObservableCollection<Survey>();
        }

        private async void NewSurveyCommandExecute()
        {
            await _navigationServiceService.NavigateAsync(nameof(SurveyDetail));
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var allSurveys = await _localDbService.GetAllSurveysAsync();
            if (Surveys != null)
            {
                Surveys = new ObservableCollection<Survey>(allSurveys);
            }
        }
    }
}
