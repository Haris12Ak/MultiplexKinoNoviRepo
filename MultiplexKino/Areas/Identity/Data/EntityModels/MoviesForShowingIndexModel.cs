namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class MoviesForShowingIndexModel
    {
        public IEnumerable<CinemaHall> CinemaHalls { get; set; }
        public IEnumerable<Projekcija> Movies { get; set; }
        public IEnumerable<Seats> Seats { get; set; }
        public IEnumerable<SeatsForHall> SeatsForHalls { get; set; }
        public IEnumerable<MoviesForShowing> MoviesForShowing { get; set; }
    }
}
