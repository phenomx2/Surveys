using System;
using System.Linq.Expressions;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Views;
using Surveys = Surveys.Core.Views.Surveys;

#pragma warning disable 618

namespace Surveys.Core.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IWebApiService _webApiService;
        private string _userName;
        private bool _isBusy;

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

        public LoginViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IWebApiService webApiService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _webApiService = webApiService;
            LoginCommand = new DelegateCommand(LoginCommandExecute,LoginCommandCanExecute)
                .ObservesProperty(() => UserName)
                .ObservesProperty(() => Password);
        }

        private bool LoginCommandCanExecute() => 
            !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

        private async void LoginCommandExecute()
        {
            IsBusy = true;
            try
            {
                var loginResult = await _webApiService.LoginAsync(UserName, Password);
                if (loginResult)
                {
                    await _navigationService.NavigateAsync($"app:///{nameof(Main)}/{nameof(RootNavigation)}/{nameof(Views.Surveys)}");
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync(Literals.LoginTitle, Literals.AccessDenied, Literals.Ok);
                }
            }
            catch (Exception exception)
            {
                await _pageDialogService.DisplayAlertAsync(Literals.LoginTitle, exception.Message, Literals.Ok);
            }
            IsBusy = false;
            
        }
    }
}