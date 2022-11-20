using System.ComponentModel.DataAnnotations;

namespace FilmApp.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Количество серий")]
        public int CountOfSeries { get; set; }
        [Display(Name = "Картинка")]
        public string Img { get; set; }
        [Display(Name = "Трейлер")]
        public string Treiler { get; set; }

        [Display(Name = "Год выхода")]
        public int Year { get; set; }

        [Display(Name = "Страна")]
        public string Country  { get; set; }

        [Display(Name = "Режисер")]
        public string Director { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; } 


    }
}
