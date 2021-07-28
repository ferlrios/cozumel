using Cozumel.Data;
using Cozumel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cozumel.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserManagerController(UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Where(u=> u.EmailConfirmed == false).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Verify(string userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault() ;
            user.EmailConfirmed = true;
            _context.Update(user);
            await _context.SaveChangesAsync();
            var users = await _userManager.Users.Where(u => u.EmailConfirmed == false).ToListAsync();
            return View("Index", users);
        }

        public IActionResult Details(string userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            return View(user);
        }
    }
}
