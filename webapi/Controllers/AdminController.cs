using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly LoginData loginData;

        public AdminController(LoginData context)
        {
            loginData = context;
        }
        [HttpGet]
        public async Task<IActionResult> getAdmin()
        {
            var admin = await loginData.admins.ToListAsync();
            return Ok(admin);
        }
        [HttpGet("{emailId}/{pwd}")]
        public async Task<IActionResult> verif(string emailId , string pwd)
        {
            
            var admin = await loginData.admins.FirstOrDefaultAsync(a => a.Email == emailId);
            if(admin == null)
            {
                return NotFound("user not found");
            }
            if (admin.Password == pwd)
            {
                if (admin.isAdmin == 1)
                {
                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }
            else
            {
                return Unauthorized("Invalid Password");
            }

        }
        [HttpPost]
        public async Task<IActionResult> registration(AdminLogin detail)
        {
            loginData.admins.Add(detail);
            await loginData.SaveChangesAsync();
            return Ok("Successfully registrated");
        }
        [HttpPut("emailId/pwd")]

        public async Task<IActionResult> updatePassword(string emailId , string pwd)
        {
            AdminLogin user = await loginData.admins.FirstOrDefaultAsync(a => a.Email == emailId);
            if(user == null)
            {
                return BadRequest("user not found");
            }
            user.Password = pwd;
            await loginData.SaveChangesAsync();
            return Ok("password updated successfully");
        }

    }
}
