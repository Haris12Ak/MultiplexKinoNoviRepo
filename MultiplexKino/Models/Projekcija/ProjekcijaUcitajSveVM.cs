using MultiplexKino.Models.Film;
using System.ComponentModel;

namespace MultiplexKino.Models.Projekcija
{
    public class ProjekcijaUcitajSveVM
    {
        public int Id { get; set; }
        public FilmPrikazPocetnaVM Film { get; set; }

        [DisplayName("Vrijeme prikazivanja filma")]
        public string VrijemeDo { get; set; }

        public string? SearchString { get; set; }

    }
}
