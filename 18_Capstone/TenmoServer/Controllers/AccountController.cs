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
    public class AccountController : ControllerBase
    {
        private IAccountDAO accountDAO;

        public AccountController(IAccountDAO _accountDAO)
        {
            accountDAO = _accountDAO;
        }

        [HttpGet("{userId}")]
        public IActionResult GetAccount(int userId)
        {
            Account account = accountDAO.GetAccount(userId);

            return Ok(account);
        }
    }
}
