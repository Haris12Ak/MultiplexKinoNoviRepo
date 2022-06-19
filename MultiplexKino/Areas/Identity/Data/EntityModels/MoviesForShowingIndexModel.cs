namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class MoviesForShowingIndexModel
    {
        public IEnumerable<Film> Filmovi { get; set; }
        public IEnumerable<Projekcija> Projekcije { get; set; }
        public IEnumerable<Sala> Sele { get; set; }
        public IEnumerable<Sjedalo> Sjedala { get; set; }
    }
}
