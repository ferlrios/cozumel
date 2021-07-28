using Cozumel.Data;
using Cozumel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Cozumel.Controllers
{
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        private readonly ApplicationDbContext _context;

        private readonly UserManager<MyUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            //Voy a guardar en today el dia de hoy y en lastday la fecha dentro de 14 días.
            int flagDay = 0;
            DateTime lastday;
            DateTime today = DateTime.Now;
            if (today.Month == 1 || today.Month == 3 || today.Month == 5 || today.Month == 7 || today.Month == 8 || today.Month == 10 || today.Month == 12)
            {
                flagDay = 31;
                if (today.Month == 12)
                {
                    if (today.Day <= 16)
                    {
                        lastday = new DateTime(today.Year, today.Month, (today.Day + 13));
                    }else
                    {
                        lastday = new DateTime(today.Year + 1, 1, (13 + today.Day - 31));
                    }

                } else
                {
                    if (today.Day <= 16)
                    {
                        lastday = new DateTime(today.Year, today.Month, (today.Day + 13));
                    }else
                    {
                        lastday = new DateTime(today.Year, today.Month + 1, (13 + today.Day - 31));
                    }
                }
            } else if (today.Month == 4 || today.Month == 6 || today.Month == 9 || today.Month == 11)
            {
                flagDay = 30;
                if (today.Day <= 15)
                {
                    lastday = new DateTime(today.Year, today.Month, (today.Day + 13));
                } else
                {
                    lastday = new DateTime(today.Year, today.Month + 1, (13 + today.Day - 30));
                }

            } else
            {
                flagDay = 28;
                if (today.Day <= 13)
                {
                    lastday = new DateTime(today.Year, 2, (today.Day + 13));
                }
                else
                {
                    lastday = new DateTime(today.Year, 3, (13 + today.Day - 28));
                }
            }

            int[] daysList = new int[14];
            for (var i=0; i <=13; i++)
            {
                daysList[i] = today.Day + i;
                switch (flagDay)
                {
                    case 28:
                        if (daysList[i] > 28) {
                            daysList[i] = daysList[i] - 28;
                        }
                        break;
                    case 30:
                        if (daysList[i] > 30)
                        {
                            daysList[i] = daysList[i] - 30;
                        }
                        break;
                    case 31:
                        if (daysList[i] > 31)
                        {
                            daysList[i] = daysList[i] - 31;
                        }
                        break;
                }
            }
            ViewBag.Days = daysList;
            ViewBag.Month = DateTime.Today.Month;
            ViewBag.DayName = (int) DateTime.Today.DayOfWeek;
            string[] week = new string[7] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };
            ViewBag.Week = week;            
            var events = await (_context.Events.Include(c => c.RelatedUser).Where(e => (e.Date.DayOfYear >= today.DayOfYear) && (e.Date.DayOfYear <= lastday.DayOfYear))).ToListAsync();
            return View(events);
        }

        public IActionResult RegisterChoice()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MessageToAdmin()
        {
            return View();
        }

        public IActionResult ContactAdmin(string Message)
        {
            var user = _userManager.GetUserName(HttpContext.User);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 25);
            client.EnableSsl = true;
            MailMessage mailMessage = new MailMessage("cozumel.arg@gmail.com", "cozumel.arg@gmail.com");
            mailMessage.Subject = $"Mensaje al admin de {user} ";
            mailMessage.Body = $"{Message}";
            client.Credentials = new NetworkCredential("cozumel.arg@gmail.com", "Cozumel.1234");
            client.Send(mailMessage);
            return RedirectToAction("AdminContacted");
        }

        public IActionResult AdminContacted ()
        {
            return View();
        }
    }
}
