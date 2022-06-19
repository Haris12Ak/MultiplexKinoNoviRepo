using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiplexKino.Models.Projekcija
{
    public class ProjekcijaSnimiVM
    {
        public int Id { get; set; }

        public int FilmId { get; set; }

        public int SalaId { get; set; }


        [DisplayName("Cijena (KM)")]
        [Required(ErrorMessage = "Polje je obavezno.")]
        [RegularExpression(@"[\d]{1,4}([.,][\d]{1,2})?", ErrorMessage = "Molimo unesite validnu cijenu.")]
        public decimal Cijena { get; set; }

        [DisplayName("Vrijedi Od")]
        [DataType(DataType.Date, ErrorMessage = "Molimo unesite validan datum.")]
        [Required(ErrorMessage = "Polje je obavezno.")]
        public DateTime VrijediOd { get; set; }

        [DisplayName("Vrijedi Do")]
        [DataType(DataType.Date, ErrorMessage = "Molimo unesite validan datum.")]
        [Required(ErrorMessage = "Polje je obavezno.")]
        public DateTime VrijediDo { get; set; }
    }
}
