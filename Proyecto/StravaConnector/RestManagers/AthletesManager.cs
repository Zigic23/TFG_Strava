using RestSharp;
using StravaConnector.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaConnector.RestManagers
{
    public class AthletesManager
    {
        public string stravaUrl { get; set; }

        public AthletesManager(string stravaUrl)
        {
            this.stravaUrl = stravaUrl;
        }

        public Athlete GetLoggedAthlete(string user_token)
        {
            RestClient client = new RestClient(stravaUrl);

            RestRequest request = new RestRequest("/api/v3/athlete", Method.GET);

            request.AddHeader("Authorization", $"Bearer {user_token}");

            IRestResponse<Athlete> response = client.Execute<Athlete>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return response.Data;
            else
                return null;
        }
    }
}
