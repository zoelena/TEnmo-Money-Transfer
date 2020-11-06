using RestSharp;
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
        //private static API_Account account = new API_Account();
        //private object registerUser;

        public decimal GetAccountBalance(int userId)
        {
            RestRequest request = new RestRequest($"{API_BASE_URL}account/{userId}");
            IRestResponse<decimal> response = client.Get<decimal>(request);
            decimal retrievedAccountBalance = response.Data;
            return retrievedAccountBalance;

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
