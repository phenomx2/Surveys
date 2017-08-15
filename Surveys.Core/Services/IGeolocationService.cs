using System;
using System.Threading.Tasks;

namespace Surveys.Core.Services
{
    public interface IGeolocationService
    {
        Task<Tuple<double, double>> GetCurrentLocationAsync();
    }
}