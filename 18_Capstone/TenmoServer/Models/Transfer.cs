﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferID {get ; set;}
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public int TransferTypeID { get; set; }
        public decimal Amount { get; set; }
        public int TransferStatusID { get; set; }
    }
}
