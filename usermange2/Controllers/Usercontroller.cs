using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using usermange2.Models;
using usermange2.viewmodel;

namespace usermange2.Controllers
{
    [Authorize(Roles="Admin")]
    public class Usercontroller : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanger;
        private readonly RoleManager<IdentityRole> _Rolemanger;
        public Usercontroller(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> Rolemanger)
        {
            _usermanger=userManager;    
            _Rolemanger=Rolemanger;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _usermanger.Users.Select(user => new UserViewmodel
            {
                Id = user.Id,

                FirstName = user.Firsrname,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _usermanger.GetRolesAsync(user).Result
            }).ToListAsync();   
            return View(user);
        }
        public async Task<IActionResult> Add()
        {
          
            var roles = await _Rolemanger.Roles.Select(e=>new RoleViewModel
            {
                RoleId=e.Id,
                RoleName=e.Name

            }).ToListAsync();
            var viewmodel = new Adduserviewmodel
            {
              
                Roles=roles
            };

            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Adduserviewmodel model)
        {
            if (!ModelState.IsValid)
                return View(model);
          
            if(!model.Roles.Any(r=> r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please select at least one role");
                return View(model);
            }

            if(await _usermanger.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is aready exists");
                return View(model);
            }

            if (await _usermanger.FindByNameAsync(model.username) != null)
            {
                ModelState.AddModelError("username", "User Name is aready exists");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.username,
                Email = model.Email,
                Firsrname = model.Firsrname,
                LastName = model.LastName
            };
            //////////////
            var result = await _usermanger.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);

            }

            await _usermanger.AddToRolesAsync(user, model.Roles.Where(r=>r.IsSelected).Select(r => r.RoleName));

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _usermanger.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var viewmodel = new ProfileFormViewModel
            {
                Id=user.Id,
                Firsrname=user.Firsrname,
                LastName=user.LastName,
                username=user.UserName,
                Email=user.LastName
            };

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _usermanger.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();

            var userwithsameemail=await _usermanger.FindByEmailAsync(user.Email);

            if (userwithsameemail != null && userwithsameemail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "this email is oready assigned to anther user ");
                return View( model);
            }

            var userwithsameuser = await _usermanger.FindByEmailAsync(user.UserName);

            if (userwithsameuser != null && userwithsameuser.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "this Use Name is oready assigned to anther user ");
                return View(model);
            }

            user.Firsrname=model.Firsrname;
            user.LastName=model.LastName;
            user.UserName=model.username;
            user.Email=model.Email;

            await _usermanger.UpdateAsync(user);

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> MangeRoles(string userId) 
        {
            var user = await _usermanger.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var roles = await _Rolemanger.Roles.ToListAsync();
            var viewmodel = new UserRolesViewModel
            {
                UserId=user.Id,
                UserName=user.UserName,
                Roles=roles.Select(role=>new RoleViewModel
                {
                    RoleId=role.Id,
                    RoleName=role.Name,
                    IsSelected=_usermanger.IsInRoleAsync(user,role.Name).Result
                }).ToList()
            };

            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MangeRoles(UserRolesViewModel model)
        {
            var user = await _usermanger.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            var userrole = await _usermanger.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if(userrole.Any(r=>r==role.RoleName) && !role.IsSelected)
                    await _usermanger.RemoveFromRoleAsync(user,role.RoleName);
                if (!userrole.Any(r => r == role.RoleName) && role.IsSelected)
                    await _usermanger.AddToRoleAsync(user, role.RoleName);

            }

            return RedirectToAction(nameof(Index));

        }
    }
}
