using FilmApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmApp.Controllers
{
    public class OverviewController : Controller
    {
        private FilmContext db;
        public OverviewController(FilmContext context)
        {
            db = context;
        }
        public IActionResult Info(int id)
        {
            Film film = db.Films.Find(id);
            return View(film);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
