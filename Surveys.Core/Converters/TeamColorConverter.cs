using System;
using System.Globalization;
using Xamarin.Forms;

namespace Surveys.Core.Converters
{
    public class TeamColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
                throw new ArgumentException("Value cannot be null");
            var team = (string) value;
            Color color;

            switch (team)
            {
                case "América":
                case "Peñarol":
                    color = Color.Yellow;
                    break;
                case "Boca Juniors":
                case "Colo-Colo":
                case "Alianza Lima":
                    color = Color.Blue;
                    break;
                case "Caracas FC":
                case "Saprissa":
                    color = Color.Purple;
                    break;
                case "Real Madrid":
                        color = Color.Fuchsia;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}