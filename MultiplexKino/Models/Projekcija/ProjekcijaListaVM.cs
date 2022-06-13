using System.ComponentModel;

namespace MultiplexKino.Models.Projekcija
{
    public class ProjekcijaListaVM
    {
        public int Id { get; set; }
        public string Plakat { get; set; }
        public string Film { get; set; }
        public string Sala { get; set; }

        [DisplayName("Cijena (KM)")]
        public string Cijena { get; set; }

        [DisplayName("Vrijedi (Od - Do)")]
        public string VrijediOdDo { get; set; }
    }
}
