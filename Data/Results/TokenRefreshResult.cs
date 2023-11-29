using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Data.Results
{
    public class TokenRefreshResult
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }

}
