﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Unity;
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
            await NavigationService.NavigateAsync($"{nameof(RootNavigation)}/{nameof(Views.Surveys)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation < Views.RootNavigation>();
            Container.RegisterTypeForNavigation<Views.Surveys>();
            Container.RegisterTypeForNavigation<Views.SurveyDetail>();
        }
    }
}