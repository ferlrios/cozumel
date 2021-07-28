using Cozumel.Data;
using Cozumel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        [Authorize(Roles = "Admin, SuperAdmin, Moderator")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {

            var list = await _context.Events.ToListAsync();
            return View("List", list);
        }

        [Authorize(Roles = "Admin, SuperAdmin, Moderator")]
        public async Task<IActionResult> Create([Bind("Title, Description, Date, Price, Address")] Event NewEvent, int gorra)
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                NewEvent.RelatedUserID = userId;
                if (gorra == 1)
                {
                    NewEvent.Price = -1;
                }
                _context.Add(NewEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View("Index",NewEvent);
        }

        public IActionResult Details(int Id)
        {
            var modelEvent = _context.Events.First(e => e.ID == Id);
            return View(modelEvent);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var modelEvent = _context.Events.First(e => e.ID == Id);
            _context.Events.Remove(modelEvent);
            await _context.SaveChangesAsync();
            var list = await _context.Events.ToListAsync();
            return View("List", list);
        }

        public async Task<IActionResult> DeletePast()
        {
            var modelEvent = _context.Events.Where(e => e.Date < DateTime.Today);
            foreach(var item in modelEvent)
            {
            _context.Events.Remove(item);
            }
            await _context.SaveChangesAsync();
            var list = await _context.Events.ToListAsync();
            return View("List", list);
        }

        public IActionResult Report(int id)
        {
            return View(id);
        }

        public IActionResult SendReport(string Message, int id)
        {
            var eventModel = _context.Events.Where(e => e.ID == id).FirstOrDefault();
            var user = _userManager.GetUserName(HttpContext.User);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 25);
            client.EnableSsl = true;
            MailMessage mailMessage = new MailMessage("cozumel.arg@gmail.com", "cozumel.arg@gmail.com");
            mailMessage.Subject = $"El usuario {user} está reportando al evento {eventModel.Title}";
            mailMessage.Body = $"{Message}";
            client.Credentials = new NetworkCredential("cozumel.arg@gmail.com", "Cozumel.1234");
            client.Send(mailMessage);
            return RedirectToAction("AdminContacted", "Home");
        }

        public async Task<IActionResult> Day(int day, int month)
        {
            var listEvents = await _context.Events.Where(e => e.Date.Day == day && e.Date.Month == month).ToListAsync();
            return View(listEvents);
        }
    }
}
