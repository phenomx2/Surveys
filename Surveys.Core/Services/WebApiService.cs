using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Surveys.Core.ServiceInterfaces;
using Surveys.Entities;

namespace Surveys.Core.Services
{
    public class WebApiService : IWebApiService
    {
        private readonly HttpClient _client;

        public WebApiService()
        {
            _client = new HttpClient{ BaseAddress = new Uri(Literals.WebApiServiceAddress)};
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            IEnumerable<Team> result = new List<Team>();
            var teams = await _client.GetStringAsync("api/teams");

            if (!string.IsNullOrWhiteSpace(teams))
                result = JsonConvert.DeserializeObject<IEnumerable<Team>>(teams);

            return result;
        }

        public async Task<bool> SaveSurveysAsync(IEnumerable<Survey> surveys)
        {
            var content = new StringContent(JsonConvert.SerializeObject(surveys),Encoding.UTF8,"application/json");
            var response = await _client.PostAsync("api/surveys", content);
            return response.IsSuccessStatusCode;
        }
    }
}