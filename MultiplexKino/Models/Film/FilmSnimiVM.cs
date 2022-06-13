using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Models.Film
{
    public class FilmSnimiVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [MaxLength(250, ErrorMessage = "Naziv ne može biti duži od 250 karaktera.")]
        public string Naslov { get; set; }

        public int? Trajanje { get; set; }

        public int? GodinaSnimanja { get; set; }

        public string Reditelj { get; set; }

        public string Glumci { get; set; }

        public string Sadrzaj { get; set; }

        public string VideoLink { get; set; }

        public string Plakat { get; set; }

        public int ZanrId { get; set; }
    }
}
