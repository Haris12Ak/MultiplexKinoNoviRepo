using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class Film
    {
        public Film()
        {
            this.Projekcije = new HashSet<Projekcija>();
        }

        public int Id { get; set; }

        [MaxLength(250)]
        public string Naslov { get; set; }

        public int? Trajanje { get; set; }

        public int? GodinaSnimanja { get; set; }

        public string Reditelj { get; set; }

        public string Glumac { get; set; }

        [MaxLength(2000)]
        public string Sadrzaj { get; set; }

        [MaxLength(100)]
        public string VideoLink { get; set; }

        public byte[] Plakat { get; set; }

        public int ZanrId { get; set; }

        public virtual Zanr Zanr { get; set; }

        public virtual ICollection<Projekcija> Projekcije { get; set; }
    }
}
