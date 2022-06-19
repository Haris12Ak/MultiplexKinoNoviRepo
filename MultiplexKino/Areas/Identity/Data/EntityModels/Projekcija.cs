namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class Projekcija
    {
        public int Id { get; set; }

        public int FilmId { get; set; }

        public int SalaId { get; set; }

        public decimal Cijena { get; set; }

        public DateTime Datum { get; set; }

        public DateTime VrijediOd { get; set; }

        public DateTime VrijediDo { get; set; }

        public virtual Film Film { get; set; }

        public virtual Sala Sala { get; set; }


        //public virtual ICollection<ProjekcijaRaspored> Rasporedi { get; set; }

    }
}
