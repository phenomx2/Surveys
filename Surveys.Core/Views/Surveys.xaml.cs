using Surveys.Core.ViewModel;
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
            MessagingCenter.Subscribe<SurveysViewModel>(this,Messages.NewSurvey, async (sender) =>
            {
                await Navigation.PushAsync(new SurveyDetail());
            });
        }
    }
}