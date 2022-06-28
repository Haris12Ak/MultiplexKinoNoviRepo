using Microsoft.AspNetCore.Mvc;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Areas.Identity.Data.EntityModels;
using MultiplexKino.Models.Naplata;
using MultiplexKino.Services;

namespace MultiplexKino.Controllers
{
    public class NaplataController : Controller
    {
        private readonly MultiplexKinoDbContext _db;
        private IEmailLogSender _emailLogSender;

        public NaplataController(MultiplexKinoDbContext db, IEmailLogSender emailLogSender)
        {
            _db = db;
            _emailLogSender = emailLogSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(NaplataSnimiVM model)
        {
            try
            {
                _db.Rezervacija.Add(new Rezervacija
                {
                    Ime = model.Ime,
                    Prezime = model.Prezime,
                    BrojLicneKarte = model.BrojLicneKarte,
                    BrojKarata = model.BrojKarata,
                    ImePrezimeNaKartici = model.ImePrezimeNaKartici,
                    Racun = model.Racun
                });

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("UcitajSveProjekcije","Projekcija");
        }
    }
}
