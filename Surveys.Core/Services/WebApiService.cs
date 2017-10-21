using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Surveys.Core.Model;
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

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var encodedUserName = WebUtility.UrlEncode(userName);
            var encodedPassword = WebUtility.UrlEncode(password);
            var content = new StringContent($"grant_type=password&username={encodedUserName}&password={encodedPassword}",
                                Encoding.UTF8,"application/x-www-form-urlencoded");
            if (!_client.DefaultRequestHeaders.Contains("keep-alive"))
            {
                _client.DefaultRequestHeaders.Add("keep-alive", "1");
            }
            var uri = new Uri($"{Literals.WebApiServiceAddress}Token");
            using (var response = _client.PostAsync(uri.ToString(),content).Result) //<-TODO Que pedo ?
            {
                var value = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var token = JsonConvert.DeserializeObject<TokenModelResponse>(value);
                    var tokenString = token.AccessToken;
                    if(!_client.DefaultRequestHeaders.Contains("Authorization"))
                        _client.DefaultRequestHeaders.Add("Authorization","Bearer "+tokenString);

                    return true;
                }
                return false;
            }

        }
    }
}