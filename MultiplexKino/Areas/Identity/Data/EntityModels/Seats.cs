using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class Seats
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SeatNumber { get; set; }
        [Required]
        public bool IsBusy { get; set; }
        public override string ToString()
        {
            return SeatNumber;
        }
    }
}
