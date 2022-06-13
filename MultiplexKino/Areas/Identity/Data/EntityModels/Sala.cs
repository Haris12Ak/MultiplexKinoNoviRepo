using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class Sala
    {
        public Sala()
        {
            this.Projekcije = new HashSet<Projekcija>();
        }

        public int Id { get; set; }

        [MaxLength(250)]
        public string Naziv { get; set; }

        public int? BrojSjedista { get; set; }

        public virtual ICollection<Projekcija> Projekcije { get; set; }
    }
}
