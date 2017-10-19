using System;
using System.Threading.Tasks;

namespace Surveys.Core.Services
{
    public interface IGeolocationService//TODO Mover a su namespace correspondiente
    {
        Task<Tuple<double, double>> GetCurrentLocationAsync();
    }
}