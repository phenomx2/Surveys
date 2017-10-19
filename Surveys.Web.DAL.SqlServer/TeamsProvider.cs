using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Surveys.Entities;

namespace Surveys.Web.DAL.SqlServer
{
    public class TeamsProvider : SqlServerProvider
    {
        public override string ConnectionString { get; set; } = System.Configuration.ConfigurationManager
            .ConnectionStrings["Surveys"].ConnectionString;

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            var result = new List<Team>();
            var query = "SELECT * FROM Teams";
            using (var reader = await ExecuteReaderAsync(query))
            {
                while (reader.Read())
                {
                    result.Add(GetTeamsFromReader(reader));
                }
            }
            return result;
        }

        private Team GetTeamsFromReader(SqlDataReader reader)
        {
            return new Team
            {
                Id = (int) reader[nameof(Team.Id)],
                Name = (string) reader[nameof(Team.Name)],
                Color = (string) reader[nameof(Team.Color)],
                Logo = (byte[]) reader[nameof(Team.Logo)]
            };
        }
    }
}