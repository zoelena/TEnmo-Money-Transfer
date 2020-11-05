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
        Transfer GetSpecificTransfer();
        void CreateNewTransfer(Transfer request);
    }
}
