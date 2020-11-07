using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
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

        public List<Transfer> GetTransfers(int transferStatus, int userId)
        {
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
            RestRequest request = new RestRequest($"{API_BASE_URL}transfer");            
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
            List<Transfer> finalTransferList = new List<Transfer>();
            if (transferStatus == 1)
            {
                foreach(Transfer toCheck in transferList)
                {
                    if ((toCheck.TransferStatusID == 1)&&(toCheck.FromId==userId))
                    {
                        finalTransferList.Add(toCheck);
                    }
                }
            }
            if (transferStatus != 1)
            {
                foreach (Transfer toCheck in transferList)
                {
                    if (toCheck.TransferStatusID != 1)
                    {
                        finalTransferList.Add(toCheck);
                    }
                }
            }

            return finalTransferList;

        }

        //public Transfer GetSpecificTransfer(int transferId)
        //{
        //    JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
        //    client.Authenticator = token;
        //    RestRequest request = new RestRequest($"{API_BASE_URL}transfer/{transferId}");
        //    IRestResponse<Transfer> response = client.Get<Transfer>(request);
        //    if (response.ResponseStatus != ResponseStatus.Completed)
        //    {
        //        throw new Exception("Error occurred - unable to reach server.");
        //    }

        //    if (!response.IsSuccessful)
        //    {
        //        throw new Exception("Authorization is required for this option. Please log in.");
        //    }
        //    Transfer specificTransfer = response.Data;
        //    return specificTransfer;

        //}
        public void NewTransfer(int userIdTo, decimal amount)
        {
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
            Transfer transfer = new Transfer
            {

                TransferTypeID = 2,
                TransferStatusID = 2,
                Amount = amount,
                ToId = userIdTo,

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
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Authorization is required for this option. Please log in.");
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception("Insufficient Funds");
                }
            }
            else
            {
                Console.WriteLine("Transfer Completed");
            }

        }
        public void NewRequest(int userIdFrom, decimal amount)
        {
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
            Transfer transfer = new Transfer
            {

                TransferTypeID = 1,
                TransferStatusID = 1,
                Amount = amount,
                FromId = userIdFrom,

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
                Console.WriteLine("Transfer Requested");
            }
        }
        public void UpdateTransferStatus(Transfer updatedTransfer)
        {
            JwtAuthenticator token = new JwtAuthenticator(UserService.GetToken());
            client.Authenticator = token;
            RestRequest request = new RestRequest($"{API_BASE_URL}transfer");
            request.AddJsonBody(updatedTransfer);
            IRestResponse<Transfer> response = client.Put<Transfer>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }

            if (!response.IsSuccessful)
            {
                throw new Exception("Authorization is required for this option. Please log in.");
            }
            string status = "";
            if (updatedTransfer.TransferStatusID == 2)
            {
                status = "Approved";
            }
            if (updatedTransfer.TransferStatusID == 3)
            {
                status = "Rejected";
            }
            Console.WriteLine($"Transfer {status}");            
        }

    }
}
