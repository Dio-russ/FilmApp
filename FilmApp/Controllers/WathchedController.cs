using FilmApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FilmApp.Controllers
{
    public class WathchedController : Controller
    {
        private readonly FilmContext _context;
        UserManager<IdentityUser> _userManager;
        public WathchedController(FilmContext context,
            UserManager<IdentityUser> manager)
        {
            _userManager = manager;
            _context = context;
        }
        public IActionResult Add(int Id, [Bind("Id,Name,CountOfSeries,Img,Treiler,Year,Country,Director,Description")] Film film)
        {

            var name = HttpContext.User.Identity.Name;
            var users = _context.Users.Where(u => u.Email == name);
            var user = users.FirstOrDefault();
            int id = user.Id;
            var temp = _context.Watcheds.Where(w => w.UserId == id && w.FilmId == Id);
            if (temp.Count() == 0)
            {
                var Watched = new Watched()
                {
                    UserId = id,
                    FilmId = Id
                };
                _context.Watcheds.Add(Watched);
                _context.SaveChanges();
                return View("AddSucsess");
            }
            else
            {
                return View();
            }

            
        }
        public IActionResult Remove(int Id, [Bind("Id,Name,CountOfSeries,Img,Treiler,Year,Country,Director,Description")] Film film)
        {
            var name = HttpContext.User.Identity.Name;
            var users = _context.Users.Where(u => u.Email == name);
            var user = users.FirstOrDefault();
            int id = user.Id;
            var temp = _context.Watcheds.Where(w => w.UserId == id && w.FilmId == Id);
            if (!(temp.Count() == 0))
            {
                var result = temp.FirstOrDefault();
                _context.Watcheds.Remove(result);
                _context.SaveChanges();
                return View("RemoveException");
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
