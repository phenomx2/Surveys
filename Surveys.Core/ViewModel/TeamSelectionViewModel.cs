using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Prism.Navigation;
using Surveys.Core.ServiceInterfaces;
#pragma warning disable 618

namespace Surveys.Core.ViewModel
{
    public class TeamSelectionViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _dbService;
        private ObservableCollection<TeamViewModel> _teams;
        private TeamViewModel _selectedTeam;

        public ObservableCollection<TeamViewModel> Teams
        {
            get => _teams;
            set
            {
                if (_teams == value) return;
                _teams = value;
                OnPropertyChanged();
            }
        }

        public TeamViewModel SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                if (_selectedTeam == value) return;
                _selectedTeam = value;
                OnPropertyChanged();
            }
        }

        public TeamSelectionViewModel(ILocalDbService dbService, INavigationService navigationService)
        {
            _dbService = dbService;
            _navigationService = navigationService;

            PropertyChanged += OnPropertyChanged;
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            var allTeams = await _dbService.GetAllTeamsAsync();
            if (allTeams != null)
            {
                Teams = new ObservableCollection<TeamViewModel>(allTeams.Select(TeamViewModel.FromEntity));
            }
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(SelectedTeam))
            {
                if(SelectedTeam == null)
                    return;
                var param = new NavigationParameters
                {
                    {"id",SelectedTeam.Id }
                };
                await _navigationService.GoBackAsync(param,true);
            }
        }
    }
}