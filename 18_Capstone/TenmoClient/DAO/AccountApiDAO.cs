using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public class AccountApiDAO
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();


        public decimal GetAccountBalance(int userId)
        {
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
            RestRequest request = new RestRequest($"{API_BASE_URL}account/{userId}");
            IRestResponse<decimal> response = client.Get<decimal>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }

            if (!response.IsSuccessful)
            {
                throw new Exception("Authorization is required for this option. Please log in.");
            }


            else
            {
                decimal retrievedAccountBalance = response.Data;
                return retrievedAccountBalance;
            }


        }

        //public int GetAccountNumber(int userId)
        //{
        //    RestRequest request = new RestRequest($"{API_BASE_URL}account/{userId}");
        //    IRestResponse<int> response = client.Get<int>(request);
        //    decimal retrievedAccountBalance = response.Data;
        //    return retrievedAccountBalance;
        //}

        
    }
}
