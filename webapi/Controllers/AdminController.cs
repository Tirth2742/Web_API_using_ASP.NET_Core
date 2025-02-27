﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Entities;

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
        [HttpGet("{userId}/{pwd}")]
        public async Task<IActionResult> verif(string userId , string pwd)
        {
            
            var admin = await loginData.admins.FirstOrDefaultAsync(a => a.userID == userId);
            if(admin == null)
            {
                return NotFound("user not found");
            }
            if (admin.Password == pwd)
            {
                return Ok(true);
            }
            else
            {
                return Unauthorized("Invalid Password");
            }

        }

    }
}
