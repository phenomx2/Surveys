using System.Collections.ObjectModel;
using System.ComponentModel;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.Model;
using Surveys.Core.Views;

namespace Surveys.Core.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ObservableCollection<Module> _modules;
        private Module _selectedModule;

        public ObservableCollection<Module> Modules
        {
            get => _modules;
            set
            {
                if (_modules == value) return;
                _modules = value;
                OnPropertyChanged();
            }
        }

        public Module SelectedModule
        {
            get => _selectedModule;
            set
            {
                if (_selectedModule == value) return;
                _selectedModule = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Modules = new ObservableCollection<Module>
            {
                new Module
                {
                    Icon = "survey.png", Title = "Encuestas",
                    LoadModuleCommand = new DelegateCommand(async 
                    () => await _navigationService.NavigateAsync($"{nameof(RootNavigation)}/{nameof(Views.Surveys)}"))
                },
                new Module
                {
                    Icon = "about.png", Title = "Acerca de",
                    LoadModuleCommand = new DelegateCommand(async
                        () => await _navigationService.NavigateAsync($"{nameof(RootNavigation)}/{nameof(Views.About)}"))
                }
            };
            PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedModule))
                SelectedModule?.LoadModuleCommand.Execute(null);
        }
    }
}