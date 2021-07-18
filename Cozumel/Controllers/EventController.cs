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
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<MyUser> _userManager;

        public EventController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {

            var list = await _context.Events.ToListAsync();
            return View("List", list);
        }

        public async Task<IActionResult> Create([Bind("Title, Description, Date, Price, Address")] Event NewEvent)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                NewEvent.RelatedUserID = userId;
                _context.Add(NewEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(NewEvent);
        }
    }
}
