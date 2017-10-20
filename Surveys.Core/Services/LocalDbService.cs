using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Surveys.Core.ServiceInterfaces;
using Surveys.Entities;
using Xamarin.Forms;

namespace Surveys.Core.Services
{
    public class LocalDbService : ILocalDbService
    {
        private readonly SQLiteConnection _connection;

        public LocalDbService()
        {
            _connection = DependencyService.Get<ISqliteService>().GetConnection();
            CreateDatabase();
        }
        
        private void CreateDatabase()
        {
            if (_connection.TableMappings.All(t => t.TableName != nameof(Survey)))
            {
                _connection.CreateTable<Survey>();
            }
            if (_connection.TableMappings.All(t => t.TableName != nameof(Team)))
            {
                _connection.CreateTable<Team>();
            }
        }

        public Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            return Task.Run(() => (IEnumerable<Survey>) _connection.Table<Survey>().ToArray());
        }

        public Task InsertSurveyAsync(Survey survey)
        {
            return Task.Run(() => _connection.Insert(survey));
        }

        public Task DeleteSurveyAsync(Survey survey)
        {
            return Task.Run(() =>
            {
                var query = $"DELETE FROM Survey WHERE Id = '{survey.Id}'";
                var command = _connection.CreateCommand(query);
                var result = command.ExecuteNonQuery();
                return result > 0;
            });
        }

        public Task DeleteAllSurveysAsync()
        {
            return Task.Run(() => _connection.DeleteAll<Survey>() > 0);
        }

        public Task DeleteAllTeamsAsync()
        {
            return Task.Run(() => _connection.DeleteAll<Team>() > 0);
        }

        public Task InsertTeamsAsync(IEnumerable<Team> teams)
        {
            return Task.Run(() => _connection.InsertAll(teams));
        }

        public Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return Task.Run(() => (IEnumerable<Team>) _connection.Table<Team>().ToArray());
        }
    }
}