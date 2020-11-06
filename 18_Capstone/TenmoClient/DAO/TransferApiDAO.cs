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
            RestRequest request = new RestRequest($"{API_BASE_URL}transfer/{userId}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }

            if (!response.IsSuccessful)
            {
                throw new Exception("Authorization is required for this option. Please log in.");
            }
            List<Transfer> transferList = response.Data;
            return transferList;

        }

        public void NewTransfer(int accountTo, int accountFrom, decimal amount)
        {
            Transfer transfer = new Transfer
            {

                TransferTypeID = 1,
                TransferStatusID = 1,
                Amount = amount,
                AccountTo = accountTo,
                AccountFrom = accountFrom

            };
            RestRequest request = new RestRequest($"{API_BASE_URL}transfer");
            request.AddJsonBody(transfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);
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
                Console.WriteLine("Transfer Completed");
            }

        }

    }
}
