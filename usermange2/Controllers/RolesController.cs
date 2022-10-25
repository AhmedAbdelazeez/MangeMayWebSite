using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using usermange2.viewmodel;

namespace usermange2.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanger;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _rolemanger = roleManager;
        }
       
        
        public async Task<IActionResult>  Index()
        {
            var roles = await _rolemanger.Roles.ToListAsync(); 
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Roleformviewmodel model)
        {
            //not clean
            if (!ModelState.IsValid)
                return View("Index", await _rolemanger.Roles.ToListAsync());
         
            var roleisvalied=await _rolemanger.RoleExistsAsync(model.Name);

            if (roleisvalied)
            { 
                ModelState.AddModelError("Name", "role is exist!");
                return View("Index", await _rolemanger.Roles.ToListAsync());
            }

            await _rolemanger.CreateAsync(new IdentityRole(model.Name.Trim()));

            return RedirectToAction (nameof(Index));



        }
    }
}
