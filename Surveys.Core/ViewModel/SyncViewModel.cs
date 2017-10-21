using System;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.ServiceInterfaces;
using Xamarin.Forms;

#pragma warning disable 618

namespace Surveys.Core.ViewModel
{
    public class SyncViewModel : ViewModelBase
    {
        private readonly IWebApiService _webApiService;
        private readonly ILocalDbService _dbService;
        private string _status;
        private bool _isBusy;


        public string Status
        {
            get => _status;
            set
            {
                if (_status == value) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ICommand SyncCommand { get; set; }

        public SyncViewModel(ILocalDbService dbService, IWebApiService webApiService)
        {
            _dbService = dbService;
            _webApiService = webApiService;

            SyncCommand = new DelegateCommand(SyncCommandExecute);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Status = Application.Current.Properties.ContainsKey("lastSync")
                ? $"Última actualización: {(DateTime) Application.Current.Properties["lastSync"]}"
                : "No se han sincronizado los datos";
        }

        private async void SyncCommandExecute()
        {
            try
            {
                IsBusy = true;
                /*Enviar encuestas*/
                var allSurveys = await _dbService.GetAllSurveysAsync();
                if (allSurveys != null && allSurveys.Any())
                {
                    await _webApiService.SaveSurveysAsync(allSurveys);
                    await _dbService.DeleteAllSurveysAsync();
                }
                /*Consultar equipos*/
                var allTeams = await _webApiService.GetTeamsAsync();
                if (allTeams != null && allTeams.Any())
                {
                    await _dbService.DeleteAllTeamsAsync();
                    await _dbService.InsertTeamsAsync(allTeams);
                }
                Application.Current.Properties["lastSync"] = DateTime.Now;
                await Application.Current.SavePropertiesAsync();
                Status = $"Se enviaron {allSurveys.Count()} encuestas y se obtuvieron {allTeams.Count()} equipos";
                IsBusy = false;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debugger.Break();
                throw;
            }
        }
    }
}