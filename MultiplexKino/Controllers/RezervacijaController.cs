using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Areas.Identity.Data.EntityModels;
using MultiplexKino.Models.Rezervacija;
using System.Text;

namespace MultiplexKino.Controllers
{
    public class RezervacijaController : Controller
    {
        private readonly MultiplexKinoDbContext _db;
        public RezervacijaController(MultiplexKinoDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public MoviesForShowingIndexModel MoviesForShowingData { get; set; }
        public int MoviesId { get; set; }

        public IActionResult Index(string id)
        {
            var sala = _db.Sala.
                Include(x => x.Sjedalo).
                Include(x => x.Projekcije).
                FirstOrDefault(x => x.Naziv == id);

            SjedaloModel sjedala = new SjedaloModel();

            if (sala != null)
            {
                return View(new RezervacijaUcitajVM
                {
                    Id = sala.Id,
                    Naziv = sala.Naziv,
                    BrojSjedista = sala.BrojSjedista,
                    Sjedala = sala.Sjedalo.ToList<Sjedalo>(),
                    Foods = _db.Food.ToList()
                });
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [BindProperty]
        public List<int> SeatsChecked { get; set; } = new List<int>();

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            foreach (var s in SeatsChecked)
            {
                var seat = await _db.Sjedalo.FindAsync(s);
                if (seat == null)
                    return NotFound();
                if (!seat.IsZauzeto)
                    seat.IsZauzeto = true;

                _db.Sjedalo.Update(seat);
                await _db.SaveChangesAsync();
            }

            SeatsChecked.Clear();
            return RedirectToAction("Index");
        }

        //public async Task Rezervisi(int? id)
        //{
        //    MoviesForShowingData = new MoviesForShowingIndexModel();

        //    MoviesForShowingData.Projekcije = await _db.Projekcija
        //        .Include(x => x.Film)
        //        .Include(x => x.Sala)
        //        .ThenInclude(x => x.Sjedalo)
        //        .ToListAsync();


        //    if (id != null)
        //        MoviesId = (int)id;

        //}
    }
}
