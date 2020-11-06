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
    public class UserController : ControllerBase
    {
        private IUserDAO userDAO;
        public UserController(IUserDAO _userDAO)
        {
            userDAO = _userDAO;
        }

        [HttpGet]
        public List<User> GetAllUsers()
        {
            return userDAO.GetUsers();
        }
    }
}
