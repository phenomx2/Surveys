using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Core.Model;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Services;
using Xamarin.Forms;
using DependencyService = Xamarin.Forms.DependencyService;
using Surveys.Entities;

namespace Surveys.Core.ViewModel
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly ILocalDbService _localDbService;
        private string _name;
        private DateTime _birthdate;
        private string _favoriteTeam;
        private ObservableCollection<string> _teams;

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

        public ObservableCollection<string> Teams
        {
            get => _teams;
            set
            {
                if (_teams == value) return;
                _teams = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILocalDbService localDbService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _localDbService = localDbService;
            Teams = new ObservableCollection<string>
            {
                "Alianza Lima",
                "América",
                "Boca Juniors",
                "Caracas FC",
                "Colo-Colo",
                "Peñarol",
                "Real Madrid",
                "Saprissa"
            };

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
                //TeamId = FavoriteTeam,
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
            var favoriteTeam = await _pageDialogService.DisplayActionSheetAsync(Literals.FavoriteTeamTitle, null, null, Teams.ToArray());
            FavoriteTeam = favoriteTeam;
        }

        private bool EndSurveyCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam);
        }
    }
}