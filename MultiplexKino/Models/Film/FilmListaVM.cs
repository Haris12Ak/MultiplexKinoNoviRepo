using System.ComponentModel;

namespace MultiplexKino.Models.Film
{
    public class FilmListaVM
    {
        public int Id { get; set; }

        public string Naslov { get; set; }

        public int? Trajanje { get; set; }

        [DisplayName("Godina snimanja")]
        public int? GodinaSnimanja { get; set; }

        public string Reditelj { get; set; }

        public string Zanr { get; set; }

        public string Plakat { get; set; }
    }
}
