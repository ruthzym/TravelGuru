using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Saitynai.Models;

namespace Saitynai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration configuration; 

        public LoginController(IConfiguration config)
        {
            configuration = config;
        }
        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            User login = new User();
            return null;
        }

    }
}
