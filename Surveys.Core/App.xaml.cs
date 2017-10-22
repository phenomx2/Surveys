using Microsoft.Practices.Unity;
using Prism.Unity;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Services;
using Surveys.Core.ViewModel;
using Xamarin.Forms.Xaml;
using Surveys.Core.Views;

namespace Surveys.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class App : PrismApplication
    {
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"{nameof(Login)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation < RootNavigation>();
            Container.RegisterTypeForNavigation<Views.Surveys,SurveysViewModel>();
            Container.RegisterTypeForNavigation<SurveyDetail,SurveyDetailsViewModel>();
            Container.RegisterTypeForNavigation<Login, LoginViewModel>();
            Container.RegisterTypeForNavigation<Main,MainViewModel>();
            Container.RegisterTypeForNavigation<About, AboutViewModel>();
            Container.RegisterTypeForNavigation<Sync, SyncViewModel>();
            Container.RegisterTypeForNavigation<TeamSelection, TeamSelectionViewModel>();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterInstance<ILocalDbService>(new LocalDbService());
            Container.RegisterInstance<IWebApiService>(new WebApiService());
        }
    }
}