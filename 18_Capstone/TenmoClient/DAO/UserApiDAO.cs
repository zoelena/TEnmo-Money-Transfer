using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.DAO
{
    public class UserApiDAO
    {

        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();

        public List<User> GetUsers()
        {
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
            RestRequest request = new RestRequest($"{API_BASE_URL}user");
            IRestResponse<List<User>> response = client.Get<List<User>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }

            if (!response.IsSuccessful)
            {
                throw new Exception("Authorization is required for this option. Please log in.");
            }
            List<User> userList = response.Data;
            return userList;

        }
    }
}
