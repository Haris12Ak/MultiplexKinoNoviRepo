using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class SeatsForHall
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Seats> Seats { get; set; }
    }
}
