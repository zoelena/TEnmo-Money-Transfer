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

        public Transfer CreateNewTransfer()
        {
            throw new NotImplementedException();
        }

        public Transfer GetSpecificTransfer()
        {
            throw new NotImplementedException();
        }

        public List<Transfer> TransferList()
        {
            throw new NotImplementedException();
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
                Amount = Convert.ToDecimal(reader["amount"]),

            };

            return transfer;
        }
    }
}
