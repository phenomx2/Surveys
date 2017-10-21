using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using Surveys.Entities;
// ReSharper disable CompareOfFloatsByEqualityOperator
#pragma warning disable 618

namespace Surveys.Core.ViewModel
{
    public class SurveyViewModel : BindableBase
    {
        private string _id;
        private string _name;
        private DateTime _birthdate;
        private TeamViewModel _team;
        private double _latitude;
        private double _longitude;

        public string Id
        {
            get => _id;
            set
            {
                if(_id == value) return;
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

        public DateTime Birthdate
        {
            get => _birthdate;
            set
            {
                if (_birthdate == value) return;
                _birthdate = value;
                OnPropertyChanged();
            }
        }

        public TeamViewModel Team
        {
            get => _team;
            set
            {
                if (_team == value) return;
                _team = value;
                OnPropertyChanged();
            }
        }

        public double Latitude
        {
            get => _latitude;
            set
            {
                if(_latitude == value) return;
                _latitude = value;
                OnPropertyChanged();
            }
        }

        public double Longitude
        {
            get => _longitude;
            set
            {
                if (_longitude == value) return;
                _longitude = value;
                OnPropertyChanged();
            }
        }

        public static SurveyViewModel FromEntitySurvey(Survey entity, IEnumerable<Team> teams)
        {
            return new SurveyViewModel
            {
                Id = entity.Id,
                Birthdate = entity.BirthDate,
                Name = entity.Name,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Team = TeamViewModel.FromEntity(teams.FirstOrDefault(t => t.Id == entity.TeamId))
            };
        }

        public static Survey ToEntitySurvey(SurveyViewModel viewModel)
        {
            return new Survey
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                BirthDate = viewModel.Birthdate,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                TeamId = viewModel.Team.Id
            };
        }
    }
}