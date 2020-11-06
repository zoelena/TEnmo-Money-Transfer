using RestSharp;
using RestSharp.Authenticators;
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
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
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

        public Transfer GetSpecificTransfer(int transferId)
        {
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
            RestRequest request = new RestRequest($"{API_BASE_URL}transfer/{transferId}");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }

            if (!response.IsSuccessful)
            {
                throw new Exception("Authorization is required for this option. Please log in.");
            }
            Transfer specificTransfer = response.Data;
            return specificTransfer;

        }
        public void NewTransfer(int accountTo, int accountFrom, decimal amount)
        {
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
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
