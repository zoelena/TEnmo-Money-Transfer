using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;

        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void CreateNewTransfer(Transfer request)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into transfers(account_from, account_to, amount, transfer_status_id, transfer_type_id) values (@account_from, @account_to, @amount, @transferstatus, @transfertype)", connection);
                command.Parameters.AddWithValue("@account_from", request.AccountFrom);
                command.Parameters.AddWithValue("@account_to", request.AccountTo);
                command.Parameters.AddWithValue("@amount", request.Amount);
                command.Parameters.AddWithValue("@transferstatus", request.TransferStatusID);
                command.Parameters.AddWithValue("@transfertype", request.TransferTypeID);
                command.ExecuteNonQuery();
            }
            
        }

        public Transfer GetSpecificTransfer(int transferId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select * from transfer where transfer_id = @transferid", connection);
                command.Parameters.AddWithValue("@transferid", transferId);
                SqlDataReader reader = command.ExecuteReader();
                Transfer retrievedTransfer = GetTransferFromReader(reader);
                return retrievedTransfer;
            }            
        }

        public List<Transfer> TransferList(int accountId)
        {
            List<Transfer> transfers = new List<Transfer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select * from transfers where (account_from = @accountId)  or(account_to = @accountId)", connection);
                command.Parameters.AddWithValue("@accountId", accountId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    transfers.Add(GetTransferFromReader(reader));
                }
            }
            return transfers;
        }

        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer transfer = new Transfer()
            {
                TransferID = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeID = Convert.ToInt32(reader["transfer_type_id"]),
                TransferStatusID = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"])
            };

            transfer.FromName = GetName(transfer.AccountFrom);
            transfer.FromId = GetUser(transfer.AccountFrom);
            transfer.ToName = GetName(transfer.AccountTo);
            transfer.ToId = GetUser(transfer.AccountTo);
            transfer.TypeName = GetTransferType(transfer.TransferTypeID);
            transfer.StatusName = GetTransferStatus(transfer.TransferStatusID);

            return transfer;
        }
        private int GetUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select user_id from accounts where account_id = @accountid", connection);
                command.Parameters.AddWithValue("@accountid", id);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }   
        private string GetName(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select username from accounts join users on accounts.user_id = users.user_id where account_id = @accountid", connection);
                command.Parameters.AddWithValue("@accountid", id);
               return Convert.ToString(command.ExecuteScalar());
            }
        }
        private string GetTransferType(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select transfer_type_desc from transfer_types where transfer_type_id = @typeid", connection);
                command.Parameters.AddWithValue("@typeid", id);
                return Convert.ToString(command.ExecuteScalar());
            }
        }
        private string GetTransferStatus(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select transfer_status_desc from transfer_statuses where transfer_status_id = @statusid", connection);
                command.Parameters.AddWithValue("@statusid", id);
                return Convert.ToString(command.ExecuteScalar());
            }
        }
    }
}
