namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojLicneKarte { get; set; }
        public int BrojKarata { get; set; }
        public string ImePrezimeNaKartici { get; set; }
        public string Racun { get; set; }
    }
}
