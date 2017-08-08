using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurveyDetail : ContentPage
    {
        private readonly string[] teams = 
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

        public SurveyDetail()
        {
            InitializeComponent();
        }

        private async void FavoriteTeamButton_Clicked(object sender, EventArgs e)
        {
            var favoriteTeam = await DisplayActionSheet(Literals.FavoriteTeamTitle, null, null, teams);
            if (!string.IsNullOrWhiteSpace(favoriteTeam))
                FavoriteTeamLabel.Text = favoriteTeam;
        }

        private async void Ok_Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(FavoriteTeamLabel.Text))
                return;

            var newSurvey = new Survey
            {
                Name = NameEntry.Text,
                BirthDate = BirthDatePicker.Date,
                FavoriteTeam = FavoriteTeamLabel.Text
            };
            MessagingCenter.Send((ContentPage)this,Messages.NewSurveyMessage, newSurvey);
            await Navigation.PopAsync();

        }
    }
}