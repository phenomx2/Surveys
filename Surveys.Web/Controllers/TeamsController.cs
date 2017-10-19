using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Surveys.Entities;
using Surveys.Web.DAL.SqlServer;

namespace Surveys.Web.Controllers
{
    public class TeamsController : ApiController
    {
        private readonly TeamsProvider _teamsProvider = new TeamsProvider();

        public async Task<IEnumerable<Team>> Get()
        {
            var allTeams = await _teamsProvider.GetAllTeamsAsync();
            return allTeams;
        }

    }
}
