using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public class AccountService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();
        private object registerUser;

        public API_Account GetAccount()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "account");
            // MIGHT add account number later GetAccount
            IRestResponse<API_Account> response = client.Get<API_Account>(request);
            return response.Data;


        }
    }
}
