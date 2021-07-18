using Cozumel.Data;
using Cozumel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            //Voy a guardar en today el dia de hoy y en lastday la fecha dentro de 14 días.
            DateTime lastday;
            DateTime today = DateTime.Now;
            if (today.Month == 1 || today.Month == 3 || today.Month == 5 || today.Month == 7 || today.Month == 8 || today.Month == 10 || today.Month == 12)
            {
                if (today.Month == 12)
                {
                    if (today.Day <= 16)
                    {
                        lastday = new DateTime(today.Year, today.Month, (today.Day + 14));
                    }else
                    {
                        lastday = new DateTime(today.Year + 1, 1, (31 - today.Day));
                    }

                } else
                {
                    if (today.Day <= 16)
                    {
                        lastday = new DateTime(today.Year, today.Month, (today.Day + 14));
                    }else
                    {
                        lastday = new DateTime(today.Year, today.Month + 1, (31 - today.Day));
                    }
                }
            } else if (today.Month == 4 || today.Month == 6 || today.Month == 9 || today.Month == 11)
            {
                if (today.Day <= 15)
                {
                    lastday = new DateTime(today.Year, today.Month, (today.Day + 14));
                } else
                {
                    lastday = new DateTime(today.Year, today.Month + 1, (30 - today.Day));
                }

            } else
            {
                if (today.Day <= 13)
                {
                    lastday = new DateTime(today.Year, 2, (today.Day + 14));
                }
                else
                {
                    lastday = new DateTime(today.Year, 3, (28 - today.Day));
                }
            }

            int[] daysList = new int[14];
            for (var i=0; i <=13; i++)
            {
                daysList[i] = today.Day + i;
            }
            ViewBag.Days = daysList;
            var events = await (_context.Events.Where(e => (e.Date >= today) && (e.Date <= lastday))).ToListAsync();
            return View(events);
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
    }
}
