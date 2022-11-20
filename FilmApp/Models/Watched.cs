using System.Collections.Generic;

namespace FilmApp.Models
{
    public class Watched
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FilmId { get; set; }
    }
}
