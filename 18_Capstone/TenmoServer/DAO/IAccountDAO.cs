using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        
        Account GetAccount(int userId);
        decimal GetBalance(int userId);
        void AddToBalance(int userId, decimal adjustment);
        void RemoveFromBalance(int userId, decimal balance);
    }
}
