using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferID {get ; set;}
        public int TransferTypeID { get; set; }
        public int TransferStatusID { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
        public string FromName { get; set; }
        public int FromId { get; set; }
        public string ToName { get; set; }
        public int ToId { get; set; }
        public string TypeName { get; set; }
        public string StatusName { get; set; }
    }
}
