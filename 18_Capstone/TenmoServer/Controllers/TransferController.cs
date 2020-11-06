using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private ITransferDAO transferDAO;
        private IAccountDAO accountDAO;

        public TransferController(ITransferDAO _transferDAO, IAccountDAO _accountDAO)
        {
            transferDAO = _transferDAO;
            accountDAO = _accountDAO;
        }
        private int GetUserId()
        {
            string strUserId = User.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            return String.IsNullOrEmpty(strUserId) ? 0 : Convert.ToInt32(strUserId);
        }
        [HttpPost]
        public IActionResult NewTransfer(Transfer request)
        {
            request.AccountFrom = accountDAO.GetAccount(GetUserId()).AccountID;
            request.AccountTo = accountDAO.GetAccount(request.ToId).AccountID;
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

        [HttpGet]
        public List<Transfer> ListTransfers()
        {
            int accountId = accountDAO.GetAccount(GetUserId()).AccountID;
            return transferDAO.TransferList(accountId);
        }
    }
}
