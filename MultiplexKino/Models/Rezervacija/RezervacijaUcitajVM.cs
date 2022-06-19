using MultiplexKino.Areas.Identity.Data.EntityModels;

namespace MultiplexKino.Models.Rezervacija
{
    public class RezervacijaUcitajVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? BrojSjedista { get; set; }
        public List<Sjedalo> Sjedala { get; set; }

        //public IEnumerable<RezervacijaSjedalaVM> Sjedala { get; set; }



        //public Sjedala Sjedala { get; set; }
    }
}
