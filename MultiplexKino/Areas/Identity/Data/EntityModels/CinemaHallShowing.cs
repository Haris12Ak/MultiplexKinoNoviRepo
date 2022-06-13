using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class CinemaHallShowing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ShowingTime { get; set; }
        public CinemaHall CinemaHall { get; set; }
    }
}
