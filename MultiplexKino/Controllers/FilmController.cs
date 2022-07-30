using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Areas.Identity.Data.EntityModels;
using MultiplexKino.Helpers;
using MultiplexKino.Models.Film;
using MultiplexKino.Services;

namespace MultiplexKino.Controllers
{
    public class FilmController : Controller
    {
        private MultiplexKinoDbContext _dbContext;
        private IEmailLogSender _emailLogSender;

        public FilmController(MultiplexKinoDbContext dbContext, IEmailLogSender emailLogSender)
        {
            _dbContext = dbContext;
            _emailLogSender = emailLogSender;
        }

        public IActionResult Index()
        {
            var filmovi = _dbContext.Film
                       .Select(x => new FilmListaVM
                       {
                           Id = x.Id,
                           Naslov = x.Naslov,
                           Trajanje = x.Trajanje,
                           GodinaSnimanja = x.GodinaSnimanja,
                           Reditelj = x.Reditelj,
                           Zanr = x.Zanr.Naziv,
                           Plakat = CommonHelper.GetImageBase64(x.Plakat)
                       }).ToList();

            return View(filmovi);
        }

        public IActionResult Prikaz(int id)
        {
            var film = _dbContext.Film
                        .Include(x => x.Zanr)
                        .FirstOrDefault(x => x.Id == id);
            if (film != null)
            {
                return View(new FilmPrikazVM
                {
                    Id = film.Id,
                    Naslov = film.Naslov,
                    Trajanje = film.Trajanje,
                    GodinaSnimanja = film.GodinaSnimanja,
                    Zanr = film.Zanr.Naziv,
                    Reditelj = film.Reditelj,
                    Glumci = film.Glumac,
                    Sadrzaj = film.Sadrzaj,
                    VideoLink = film.VideoLink,
                    Plakat = CommonHelper.GetImageBase64(film.Plakat)
                });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Dodaj()
        {
            ViewData["Zanrovi"] = _dbContext.Zanr
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.Id.ToString(),
                                        Text = x.Naziv
                                    });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dodaj(FilmSnimiVM model, IFormFile file)
        {
            try
            {
                ViewData["Zanrovi"] = _dbContext.Zanr
                                .Select(x => new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Naziv,
                                    Selected = (x.Id == model.ZanrId)
                                });

                _dbContext.Film.Add(new Film
                {
                    Naslov = model.Naslov,
                    Trajanje = model.Trajanje,
                    GodinaSnimanja = model.GodinaSnimanja,
                    Reditelj = model.Reditelj,
                    Glumac = model.Glumci,
                    Sadrzaj = model.Sadrzaj,
                    VideoLink = model.VideoLink,
                    ZanrId = model.ZanrId,
                    Plakat = CommonHelper.GetImageByteArray(file)
                });

                _dbContext.SaveChanges();
                TempData["AlertMessage"] = "Film uspješno dodan...!";
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Uredi(int id)
        {
            var film = _dbContext.Film.FirstOrDefault(x => x.Id == id);
            if (film != null)
            {
                ViewData["Zanrovi"] = _dbContext.Zanr
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.Id.ToString(),
                                        Text = x.Naziv
                                    });

                return View(new FilmSnimiVM
                {
                    Id = film.Id,
                    Naslov = film.Naslov,
                    Trajanje = film.Trajanje,
                    GodinaSnimanja = film.GodinaSnimanja,
                    Reditelj = film.Reditelj,
                    Glumci = film.Glumac,
                    Sadrzaj = film.Sadrzaj,
                    VideoLink = film.VideoLink,
                    Plakat = CommonHelper.GetImageBase64(film.Plakat),
                    ZanrId = film.ZanrId
                });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(FilmSnimiVM model, IFormFile file)
        {
            try
            {
                var film = _dbContext.Film.FirstOrDefault(x => x.Id == model.Id);

                if (film != null)
                {
                    film.Naslov = model.Naslov;
                    film.Trajanje = model.Trajanje;
                    film.GodinaSnimanja = model.GodinaSnimanja;
                    film.Reditelj = model.Reditelj;
                    film.Glumac = model.Glumci;
                    film.Sadrzaj = model.Sadrzaj;
                    film.VideoLink = model.VideoLink;
                    film.ZanrId = model.ZanrId;

                    var newPlakat = CommonHelper.GetImageByteArray(file);
                    if (newPlakat != null)
                    {
                        film.Plakat = newPlakat;
                    }

                    _dbContext.SaveChanges();
                    TempData["AlertMessage"] = "Film uspješno editovan...!";
                }
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
                var film = _dbContext.Film.FirstOrDefault(x => x.Id == id);

                if (film != null && !film.Projekcije.Any())
                {
                    _dbContext.Film.Remove(film);
                    _dbContext.SaveChanges();
                    TempData["AlertMessage"] = "Film uspješno uklonjen...!";
                }
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Index");
        }

        public IActionResult BrisiPlakat(int id)
        {
            try
            {
                var film = _dbContext.Film.FirstOrDefault(x => x.Id == id);

                if (film != null)
                {
                    film.Plakat = null;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Uredi", new { id = id });
        }
    }
}
