using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Services;
using Surveys.Core.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Surveys.Core.Views;

namespace Surveys.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App()
        {
            
            //MainPage = new NavigationPage(new Views.Surveys());
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"{nameof(Login)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation < Views.RootNavigation>();
            Container.RegisterTypeForNavigation<Views.Surveys,SurveysViewModel>();
            Container.RegisterTypeForNavigation<Views.SurveyDetail,SurveyDetailsViewModel>();
            Container.RegisterTypeForNavigation<Views.Login, LoginViewModel>();
            Container.RegisterTypeForNavigation<Views.Main,MainViewModel>();
            Container.RegisterTypeForNavigation<Views.About, AboutViewModel>();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterInstance<ILocalDbService>(new LocalDbService());
            Container.RegisterInstance<IWebApiService>(new WebApiService());
        }
    }
}