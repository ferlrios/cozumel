using Cozumel.Data;
using Cozumel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Cozumel.Controllers
{
    public class VerificationController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly UserManager<MyUser> _userManager;

        public VerificationController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ApplyForVerification(string Messagge)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 25);
            client.EnableSsl = true;
            MailMessage mailMessage = new MailMessage("cozumel.arg@gmail.com", "cozumel.arg@gmail.com");
            mailMessage.Subject = $"{user.UserName} solcita su VERIFICACIÓN";
            mailMessage.Body = $"Mensaje: {Messagge}";
            client.Credentials = new NetworkCredential("cozumel.arg@gmail.com", "Cozumel.1234");
            client.Send(mailMessage);
            return RedirectToAction("ContactAdmin", "Home");
        }

    }
}
