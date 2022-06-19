using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class Sjedalo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BrojSjedala { get; set; }

        public bool IsZauzeto { get; set; }

        public override string ToString()
        {
            return BrojSjedala.ToString();
        }
    }
}
