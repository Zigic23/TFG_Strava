using RestSharp;
using StravaConnector.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaConnector.RestManagers
{
    public class OAuthRestManager
    {
        public string stravaUrl { get; set; }

        public OAuthRestManager(string stravaUrl)
        {
            this.stravaUrl = stravaUrl;
        }

        public StravaAccessToken GetToken(string code, string client_id, string client_secret, string grant_type)
        {

            RestClient client = new RestClient(stravaUrl);

            RestRequest request = new RestRequest("/oauth/token", Method.POST);

            request.AddQueryParameter("client_id", client_id);
            request.AddQueryParameter("client_secret", client_secret);
            request.AddQueryParameter("code", code);
            request.AddQueryParameter("grant_type", grant_type);
            
            IRestResponse<StravaAccessToken> response = client.Execute<StravaAccessToken>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return response.Data;
            else
                return null;            
        }
    }
}
