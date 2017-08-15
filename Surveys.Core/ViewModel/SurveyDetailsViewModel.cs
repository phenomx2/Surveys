using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Surveys.Core.Model;
using Surveys.Core.Services;
using Xamarin.Forms;

namespace Surveys.Core.ViewModel
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
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
                OnPropertychanged();
            }
        }

        public DateTime Birthdate
        {
            get => _birthdate;
            set
            {
                if (_birthdate == value) return;
                _birthdate = value;
                OnPropertychanged();
            }
        }

        public string FavoriteTeam
        {
            get => _favoriteTeam;
            set
            {
                if (_favoriteTeam == value) return;
                _favoriteTeam = value;
                OnPropertychanged();
            }
        }

        public ObservableCollection<string> Teams
        {
            get => _teams;
            set
            {
                if (_teams == value) return;
                _teams = value;
                OnPropertychanged();
            }
        }

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel()
        {
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

            MessagingCenter.Subscribe<ContentPage,string>(this,Messages.TeamSelected, (sender, args) =>
            {
                FavoriteTeam = args;  
            });

            SelectTeamCommand = new Command(SelectTeamCommandExecute);
            PropertyChanged += SurveysDetailViewModel_OnPropertyChanged;
            EndSurveyCommand = new Command(EndSurveyCommandExecute, EndSurveyCommandCanExecute);
        }

        private void SurveysDetailViewModel_OnPropertyChanged(object o, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Name) || e.PropertyName == nameof(FavoriteTeam))
                (EndSurveyCommand as Command)?.ChangeCanExecute();
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey
            {
                Name = Name,
                FavoriteTeam = FavoriteTeam,
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
            MessagingCenter.Send(this,Messages.NewSurveyComplete,newSurvey);
        }

        private void SelectTeamCommandExecute()
        {
            MessagingCenter.Send(this,Messages.SelectTeam,Teams.ToArray());
        }

        private bool EndSurveyCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam);
        }


    }
}