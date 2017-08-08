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
            MessagingCenter.Subscribe<ContentPage, Survey>(this, Messages.NewSurveyMessage, (sender, args) => 
            {
                SurveysPanel.Children.Add(new Label { Text = args.ToString() } );
            });
        }

        private async void AddSurveys_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyDetail());
        }
    }
}