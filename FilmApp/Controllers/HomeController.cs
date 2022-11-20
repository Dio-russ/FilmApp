using FilmApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FilmApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private FilmContext db;
        public HomeController(FilmContext context)
        {
            db = context;
            //InitDb();
        }
        private void InitDb()
        {
            var flm1 = new Film()
            {
                Name = "Драйв",
                CountOfSeries = 1,
                Year = 2011,
                Director = "Николас Виндинг Рефн",
                Country = "США",
                Img = "https://www.film.ru/sites/default/files/movies/posters/1629855-1155758.jpg",
                Treiler = "https://www.youtube.com/embed/SDhGly0CgvQ",
                Description = "Великолепный водитель – при свете дня он выполняет каскадерские трюки на съёмочных площадках Голливуда, а по ночам ведет рискованную игру. Но один опасный контракт – и за его жизнь назначена награда. Теперь, чтобы остаться в живых и спасти свою очаровательную соседку, он должен делать то, что умеет лучше всего – виртуозно уходить от погони."

            };
            var flm2 = new Film()
            {
                Name = "Пацаны",
                CountOfSeries = 30,
                Year = 2019,
                Director = "Филип Сгриккиа",
                Country = "США",
                Img = "https://media.kg-portal.ru/tv/b/boys/posters/boys_8.jpg",
                Treiler = "https://www.youtube.com/embed/zNztReTPpOE",
                Description = "Действие сериала разворачивается в мире, где существуют супергерои. Именно они являются настоящими звездами. Их все знают и обожают. Но за идеальным фасадом скрывается гораздо более мрачный мир наркотиков и секса, а большинство героев — в жизни не самые приятные люди. Противостоит им отряд, неофициально известный как «Пацаны»."

            };
            var flm3 = new Film()
            {
                Name = "Ходячие мертвецы",
                CountOfSeries = 110,
                Year = 2010,
                Director = "Фрэнк Дарабонт",
                Country = "США",
                Img = "https://avatars.mds.yandex.net/get-kinopoisk-image/1946459/ffadc239-dfb6-4165-af23-ff8601df0a1f/3840x",
                Treiler = "https://www.youtube.com/embed/BogZZAPu_rA",
                Description = "Зомби-эпидемия захлестнула планету. Шериф Рик Граймс путешествует с семьей и небольшой группой выживших в поисках безопасного места. Но постоянный страх смерти каждый день приносит тяжелые потери, заставляя товарищей по несчастью чувствовать глубины человеческой жестокости. Рик пытается спасти близких и понимает, что всепоглощающий страх людей может быть опаснее ходячих мертвецов."


            };
            db.Films.AddRange(new[] { flm1, flm2, flm3 });
            db.SaveChanges();

        }
        public IActionResult Search(string searchString)
        {
            var result = new List<Film>();
            if (!string.IsNullOrEmpty(searchString))
            {
                Regex rg = new Regex(@"\w+", RegexOptions.IgnoreCase);
                var matches = rg.Matches(searchString);
                foreach (var word in matches)
                {
                    var list = db.Films.Where(b => b.Director.Contains(word.ToString()) || b.Name.Contains(word.ToString())).ToList();
                    result.AddRange(list);
                }
                result = (from item in result select item).Distinct().ToList();
            }
            if (result.Count == 0)
                return View("NotFound");
            return View("Index", result);
        }
        public IActionResult ShowMyFilms()
        {
            var result = new List<Film>();
            var name = HttpContext.User.Identity.Name;
            var users = db.Users.Where(u => u.Email == name);
            var user = users.FirstOrDefault();
            int id = user.Id;
            var temp = db.Watcheds.Where(w => w.UserId == id);
            foreach (var film in temp)
            {
                var list = db.Films.Where(b => b.Id == film.FilmId);
                result.AddRange(list);
            }
            return View("Filmlist", result);
        }
        public IActionResult Info()
        {
            return View(db.Films.ToList());
        }
        public IActionResult Index()
        {
            return View(db.Films.ToList());
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
