using MultiplexKino.Areas.Identity.Data.EntityModels;

namespace MultiplexKino.Models.Rezervacija
{
    public class RezervacijaUcitajVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? BrojSjedista { get; set; }
        public List<Sjedalo> Sjedala { get; set; }

        public List<Food> Foods { get; set; }

    }
}
