using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    class API_Transfer
    {
        public int TransferID { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public int TransferTypeID { get; set; }
        public decimal Amount { get; set; }
        public int TransferStatusID { get; set; }

    }
}
