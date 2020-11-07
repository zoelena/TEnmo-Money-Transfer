using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        List<Transfer> TransferList(int accountId);
        Transfer GetSpecificTransfer(int transferId);
        void CreateNewTransfer(Transfer request);
        void UpdateTransfer(Transfer request);
    }
}
