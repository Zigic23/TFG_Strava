using RestSharp;
using StravaConnector.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaConnector.RestManagers
{
    public class ActivitiesManager
    {
        public string stravaUrl { get; set; }

        public ActivitiesManager(string stravaUrl)
        {
            this.stravaUrl = stravaUrl;
        }

        public List<StravaActivity> GetUserActivities(string user_token, long? before, long? after, int page, int per_page)
        {
            RestClient client = new RestClient(stravaUrl);

            RestRequest request = new RestRequest("/api/v3/athlete/activities", Method.GET);

            request.AddHeader("Authorization", $"Bearer {user_token}");
            if(before != null)
                request.AddQueryParameter("before", before.ToString());
            if(after != null)
                request.AddQueryParameter("after", after.ToString());
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("per_page", per_page.ToString());

            IRestResponse<List<StravaActivity>> response = client.Execute<List<StravaActivity>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return response.Data;
            else
                return null;
        }

        public List<ActivityLaps> GetActivityLaps(string user_token, long activityCode)
        {
            RestClient client = new RestClient(stravaUrl);

            RestRequest request = new RestRequest($"/api/v3/activities/{activityCode}/laps", Method.GET);

            request.AddHeader("Authorization", $"Bearer {user_token}");

            IRestResponse<List<ActivityLaps>> response = client.Execute<List<ActivityLaps>>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return response.Data;
            else
                return null;
        }
    }
}
