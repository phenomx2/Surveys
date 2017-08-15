using System;
using System.Threading.Tasks;
using Surveys.Core.Services;
using Surveys.Droid.Services;
using Android.Content;
using Android.Locations;
[assembly:Xamarin.Forms.Dependency(typeof(GeolocationService))]
namespace Surveys.Droid.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly LocationManager _locationManager;

        public GeolocationService()
        {
            _locationManager = Xamarin.Forms.Forms.Context.GetSystemService(Context.LocationService) as LocationManager;
            
        }

        public Task<Tuple<double, double>> GetCurrentLocationAsync()
        {
            var provider = _locationManager.GetBestProvider(new Criteria {Accuracy = Accuracy.Fine}, true);
            var location = _locationManager.GetLastKnownLocation(provider);
            var result = new Tuple<double,double>(location.Latitude,location.Longitude);
            return Task.FromResult(result);
        }
    }
}