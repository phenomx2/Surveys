using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Services;
using Surveys.Core.Views;
using DependencyService = Xamarin.Forms.DependencyService;
using Surveys.Entities;
#pragma warning disable 618

namespace Surveys.Core.ViewModel
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalDbService _localDbService;
        private string _name;
        private DateTime _birthdate;
        private string _favoriteTeam;
        private IEnumerable<Team> _localTeams;

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthdate
        {
            get => _birthdate;
            set
            {
                if (_birthdate == value) return;
                _birthdate = value;
                OnPropertyChanged();
            }
        }

        public string FavoriteTeam
        {
            get => _favoriteTeam;
            set
            {
                if (_favoriteTeam == value) return;
                _favoriteTeam = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _localDbService = localDbService;

            SelectTeamCommand = new DelegateCommand(SelectTeamCommandExecute);
            EndSurveyCommand = new DelegateCommand(EndSurveyCommandExecute, EndSurveyCommandCanExecute)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => FavoriteTeam);
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                TeamId = _localTeams.First(t => t.Name == FavoriteTeam).Id,
                BirthDate = Birthdate
            };
            var geolocationService = DependencyService.Get<IGeolocationService>();
            if (geolocationService != null)
            {
                try
                {
                    var currentLocation = await geolocationService.GetCurrentLocationAsync();
                    newSurvey.Latitude = currentLocation.Item1;
                    newSurvey.Longitude = currentLocation.Item2;
                }
                catch (Exception exception)
                {
                    newSurvey.Latitude = 0;
                    newSurvey.Longitude = 0;
                    System.Diagnostics.Debug.WriteLine("-------------------");
                    System.Diagnostics.Debug.WriteLine("GEOLOCATION_EXCEPTION");
                    System.Diagnostics.Debug.WriteLine(exception.Message);
                    System.Diagnostics.Debug.WriteLine("-------------------");
                    throw;
                }
            }
            await _localDbService.InsertSurveyAsync(newSurvey);
            await _navigationService.GoBackAsync();
        }

        private async void SelectTeamCommandExecute()
        {
            await _navigationService.NavigateAsync(nameof(TeamSelection), useModalNavigation: true);   
        }

        private bool EndSurveyCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam);
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _localTeams = await _localDbService.GetAllTeamsAsync();

            if (parameters.ContainsKey("id"))
            {
                FavoriteTeam = _localTeams.First(t => t.Id == (int) parameters["id"]).Name;
            }
        }
    }
}