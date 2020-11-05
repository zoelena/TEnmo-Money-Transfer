using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private ITransferDAO transferDAO;
        private IAccountDAO accountDAO;

        public TransferController(ITransferDAO _transferDAO, IAccountDAO _accountDAO)
        {
            transferDAO = _transferDAO;
            accountDAO = _accountDAO;
        }

        [HttpPost]
        public IActionResult NewTransfer(Transfer request)
        {
            decimal fromBalance = accountDAO.GetAccount(request.AccountFrom).Balance;
            if (request.Amount <= fromBalance)
            {
                transferDAO.CreateNewTransfer(request);
                accountDAO.AddToBalance(request.AccountTo, request.Amount);
                accountDAO.RemoveFromBalance(request.AccountFrom, request.Amount);

                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Insufficient Funds" });
            }
        }

        [HttpGet("{accountId}")]
        public List<Transfer> ListTransfers(int accountId)
        {
            return transferDAO.TransferList(accountId);
        }
    }
}
