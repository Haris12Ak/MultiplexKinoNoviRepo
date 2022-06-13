using Microsoft.AspNetCore.Mvc;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Areas.Identity.Data.EntityModels;
using MultiplexKino.Models.Informacija;
using MultiplexKino.Services;

namespace MultiplexKino.Controllers
{
    public class InformacijaController : Controller
    {
        private MultiplexKinoDbContext _dbContext;
        private IEmailLogSender _emailLogSender;

        public InformacijaController(MultiplexKinoDbContext dbContext, IEmailLogSender emailLogSender)
        {
            _dbContext = dbContext;
            _emailLogSender = emailLogSender;
        }

        public IActionResult Index()
        {
            var informacije = _dbContext.Informacije
                .Select(x => new InformacijeListaVM
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Opis = x.Opis,
                    Kreiran = x.Kreiran.ToString("f")
                }).ToList();

            return View(informacije);
        }

        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dodaj(InformacijaSnimiVM model)
        {
            try
            {
                _dbContext.Informacije.Add(new Informacije
                {
                    Naziv = model.Naziv,
                    Opis = model.Opis,
                    Kreiran = DateTime.Now
                });

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Uredi(int id)
        {
            var informacija = _dbContext.Informacije.FirstOrDefault(x => x.Id == id);

            if (informacija != null)
            {
                return View(new InformacijaSnimiVM
                {
                    Id = informacija.Id,
                    Naziv = informacija.Naziv,
                    Opis = informacija.Opis
                });

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(InformacijaSnimiVM model)
        {
            try
            {
                var informacija = _dbContext.Informacije.FirstOrDefault(x => x.Id == model.Id);

                if(informacija != null)
                {
                    informacija.Naziv = model.Naziv;
                    informacija.Opis = model.Opis;
                }

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Brisi(int id)
        {
            try
            {
                var informacija = _dbContext.Informacije.FirstOrDefault(x => x.Id == id);

                if(informacija != null)
                {
                    _dbContext.Informacije.Remove(informacija);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Index");
        }

        public IActionResult UcitajInformacije()
        {
            var getAll = _dbContext.Informacije.ToList();

            var result = getAll
                .Select(x => new UcitajInformacijeVM
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Opis = x.Opis,
                    Kreiran = x.Kreiran.ToString("f")
                }).ToList();

            return View(result);
        }

        public IActionResult PrikaziInformaciju(int id)
        {
            var informacija = _dbContext.Informacije.FirstOrDefault(x => x.Id == id);

            if(informacija != null)
            {
                return View(new InformacijaPrikazVM
                {
                    Id = informacija.Id,
                    Naziv = informacija.Naziv,
                    Opis = informacija.Opis,
                    Kreiran = informacija.Kreiran.ToString("f")
                });
            }
            else
            {
                return RedirectToAction("UcitajInformacije");
            }
        }
    }
}
