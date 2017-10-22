using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Surveys.Entities;
using System;

namespace Surveys.Web.DAL.SqlServer
{
    public class SurveysProvider : SqlServerProvider
    {
        public override string ConnectionString { get; set; } = System.Configuration.ConfigurationManager
            .ConnectionStrings["Surveys"].ConnectionString;

        public async Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            var result = new List<Survey>();
            var query = "SELECT * FROM Surveys";
            using (var reader = await ExecuteReaderAsync(query))
            {
                while (reader.Read())
                {
                    result.Add(GetSurveyFromReader(reader));
                }
                return result;
            }
        }

        public async Task<int> InsertSurveyAsync(Survey survey)
        {
            if (survey == null)
                return 0;

            var query = "INSERT INTO Surveys (Id, Name, Birthdate, TeamId, Latitude, Longitude) VALUES " + 
                "(@Id, @Name, @Birthdate, @TeamId,@Latitude,@Longitude)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id",survey.Id),
                new SqlParameter("Name",survey.Name),
                new SqlParameter("@Birthdate",survey.BirthDate),
                new SqlParameter("@TeamId",survey.TeamId),
                new SqlParameter("@Latitude",survey.Latitude),
                new SqlParameter("@Longitude",survey.Longitude)
            };

            var result = await ExecuteNonQueryAsync(query, parameters.ToArray());
            return result;

        }

        private Survey GetSurveyFromReader(SqlDataReader reader)
        {
                return new Survey
                {
                    Id = reader[nameof(Survey.Id)].ToString(),
                    Name = reader[nameof(Survey.Name)].ToString(),
                    TeamId = (int) reader[nameof(Survey.TeamId)],
                    BirthDate = (DateTime) reader[nameof(Survey.BirthDate)],
                    Latitude = double.Parse(reader[nameof(Survey.Latitude)].ToString()),
                    Longitude = double.Parse(reader[nameof(Survey.Longitude)].ToString())
                };
        }
    }
}