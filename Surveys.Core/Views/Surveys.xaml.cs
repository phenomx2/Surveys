using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Surveys : ContentPage
    {
        public Surveys()
        {
            InitializeComponent();
        }

        private async void AddSurveys_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyDetail());
        }
    }
}