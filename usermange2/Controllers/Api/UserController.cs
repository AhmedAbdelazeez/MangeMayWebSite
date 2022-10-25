using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using usermange2.Models;

namespace usermange2.Controllers.Api
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanger;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _usermanger = userManager;
           
        }
        [HttpDelete]
        public async Task<IActionResult>  DeleteUser( string usreId)
        {
            var user=await _usermanger.FindByEmailAsync(usreId);
            if (user == null)
                return NotFound();
            var deleteuser=await _usermanger.DeleteAsync(user);

            if (!deleteuser.Succeeded)
                throw new Exception();

            return Ok();
            
        }
    }
}
