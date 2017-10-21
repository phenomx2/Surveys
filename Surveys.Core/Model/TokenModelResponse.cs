﻿using Newtonsoft.Json;

namespace Surveys.Core.Model
{
    public class TokenModelResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty(".issued")]
        public string IssuedAt { get; set; }
        [JsonProperty(".expires")]
        public string ExpiresAt { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}