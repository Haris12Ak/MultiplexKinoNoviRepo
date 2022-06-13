using MultiplexKino.Models.Film;
using System.ComponentModel;

namespace MultiplexKino.Models.Projekcija
{
    public class ProjekcijaKupiKartuVM
    {
        public int Id { get; set; }

        public FilmPrikazVM Film { get; set; }

        public string Sala { get; set; }

        [DisplayName("Cijena (KM)")]
        public string Cijena { get; set; }

        [DisplayName("Datum kreiranja")]
        public string Kreiran { get; set; }

        [DisplayName("Karte dostupne (Od - Do)")]
        public string VrijediOdDo { get; set; }
    }
}
