using System.IO;
using Prism.Mvvm;
using Surveys.Entities;
using Xamarin.Forms;
#pragma warning disable 618

namespace Surveys.Core.ViewModel
{
    public class TeamViewModel : BindableBase
    {
        private int _id;
        private string _name;
        private string _color;
        private ImageSource _logo;

        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Color
        {
            get => _color;
            set
            {
                if (_color == value) return;
                _color = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Logo
        {
            get => _logo;
            set
            {
                if(_logo == value) return;
                _logo = value;
                OnPropertyChanged();
            }
        }

        public static TeamViewModel FromEntity(Team team)
        {
            return new TeamViewModel
            {
                Id = team.Id,
                Color = team.Color,
                Name = team.Name,
                Logo = ImageSource.FromStream(() => new MemoryStream(team.Logo))
            };
        }
    }
}