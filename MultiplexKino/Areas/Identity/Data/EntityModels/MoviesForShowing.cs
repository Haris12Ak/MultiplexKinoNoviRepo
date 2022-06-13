using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class MoviesForShowing
    {
        [Key]
        public int Id { get; set; }
        public string ShowingTime { get; set; }
        public int MovieId { get; set; }
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; }
        public Projekcija Movie { get; set; }
    }
}
