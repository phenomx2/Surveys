using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.Views;
using Surveys = Surveys.Core.Views.Surveys;

namespace Surveys.Core.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName == value) return;
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if(_password == value) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(LoginCommandExecute,LoginCommandCanExecute)
                .ObservesProperty(() => UserName)
                .ObservesProperty(() => Password);
        }

        private bool LoginCommandCanExecute() => 
            !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

        private async void LoginCommandExecute()
        {
            await _navigationService.NavigateAsync(
                $"{nameof(Views.Main)}/{nameof(RootNavigation)}/{nameof(Views.Surveys)}");
        }
    }
}