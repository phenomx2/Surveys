using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Surveys.Entities;
using Surveys.Web.DAL.SqlServer;

namespace Surveys.Web.Controllers
{
    [Authorize]
    public class SurveysController : ApiController
    {
        private readonly SurveysProvider _surveysProvider = new SurveysProvider();

        public async Task<IEnumerable<Survey>> Get()
        {
            var allSurveys = await _surveysProvider.GetAllSurveysAsync();
            return allSurveys;
        }

        public async Task Post([FromBody] IEnumerable<Survey> surveys)
        {
            if (surveys == null)
                return;
            foreach (var survey in surveys)
            {   //TODO Crear nuevo metodo en surveysprovider para insertar todo de golpe
                await _surveysProvider.InsertSurveyAsync(survey);
            }
        }
    }
}
