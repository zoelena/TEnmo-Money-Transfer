using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.DAO
{
    public class TransferApiDAO
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();
        //private static API_Account account = new API_Account();
        //private object registerUser;

        public List<Transfer> GetTransfers(int userId)
        {
            RestRequest request = new RestRequest($"{API_BASE_URL}transfers/{userId}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            List<Transfer> transferList = response.Data;
            return transferList;

        }

    }
}
