using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Views;
using Surveys.Entities;
#pragma warning disable 618

namespace Surveys.Core.ViewModel
{
    public class SurveysViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationServiceService;
        private readonly IPageDialogService _pageDialogService;
        private readonly ILocalDbService _localDbService;
        private ObservableCollection<Survey> _surveys;
        private Survey _selectedSurvey;

        public bool IsEmpty => Surveys == null || !Surveys.Any();

        public ICommand NewSurveyCommand { get; set; }
        public ICommand DeleteSurveyCommand { get; set; }

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

        public SurveysViewModel(INavigationService navigationService, ILocalDbService localDbService, IPageDialogService pageDialogService)
        {
            _navigationServiceService = navigationService;
            _localDbService = localDbService;
            _pageDialogService = pageDialogService;
            NewSurveyCommand = new DelegateCommand(NewSurveyCommandExecute);
            Surveys = new ObservableCollection<Survey>();
            DeleteSurveyCommand = new DelegateCommand(DeleteSurveyCommandExecute,DeleteSurveyCommandCanExecute)
                .ObservesProperty(() => SelectedSurvey);
        }

        private bool DeleteSurveyCommandCanExecute() => 
            SelectedSurvey != null;

        private async void DeleteSurveyCommandExecute()
        {
            if (SelectedSurvey == null)
                return;
            var result = await _pageDialogService.DisplayAlertAsync(Literals.DeleteSurveyTitle,
                Literals.DeleteSurveyConfirmation, Literals.Ok, Literals.Cancel);
            if (result)
            {
                await _localDbService.DeleteSurveyAsync(SelectedSurvey);
                await LoadSurveys();
            }
        }

        private async void NewSurveyCommandExecute()
        {
            await _navigationServiceService.NavigateAsync(nameof(SurveyDetail));
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await LoadSurveys();
        }

        private async System.Threading.Tasks.Task LoadSurveys()
        {
            var allSurveys = await _localDbService.GetAllSurveysAsync();
            if (Surveys != null)
                Surveys = new ObservableCollection<Survey>(allSurveys);

            OnPropertyChanged(nameof(IsEmpty));
        }
    }
}
