using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class CinemaHall
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tip { get; set; }
        public int SeatsForHallId { get; set; }
        public SeatsForHall SeatsForHall { get; set; }
    }
}
