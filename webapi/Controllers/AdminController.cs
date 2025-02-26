using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;

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
    }
}
