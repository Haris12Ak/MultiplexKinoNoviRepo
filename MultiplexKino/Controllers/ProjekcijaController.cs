using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Areas.Identity.Data.EntityModels;
using MultiplexKino.Helpers;
using MultiplexKino.Models.Film;
using MultiplexKino.Models.Projekcija;
using MultiplexKino.Services;
using System.Collections.Generic;

namespace MultiplexKino.Controllers
{
    public class ProjekcijaController : Controller
    {
        private MultiplexKinoDbContext _dbContext;
        private IEmailLogSender _emailLogSender;

        public ProjekcijaController(MultiplexKinoDbContext dbContext, IEmailLogSender emailLogSender)
        {
            _dbContext = dbContext;
            _emailLogSender = emailLogSender;
        }

        public IActionResult Index()
        {
            var projekcija = _dbContext.Projekcija
                .Select(x => new ProjekcijaListaVM
                {
                    Id = x.Id,
                    Plakat = CommonHelper.GetImageBase64(x.Film.Plakat),
                    Film = x.Film.Naslov,
                    Sala = x.Sala.Naziv,
                    Cijena = x.Cijena.ToString("N"),
                    VrijediOdDo = $"{x.VrijediOd.ToString("dd.MM.yyyy")} - {x.VrijediDo.ToString("dd.MM.yyyy")}"
                }).ToList();

            return View(projekcija);
        }

        public IActionResult Dodaj()
        {
            ViewData["Sale"] = _dbContext.Sala
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                });

            ViewData["Filmovi"] = _dbContext.Film
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naslov
                });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dodaj(ProjekcijaSnimiVM model)
        {
            try
            {
                VaildProjekcija(model);

                if (!ModelState.IsValid)
                {
                    LoadViewData(model);
                    return View(model);
                }
                else
                {
                    _dbContext.Projekcija.Add(new Projekcija
                    {
                        FilmId = model.FilmId,
                        SalaId = model.SalaId,
                        Cijena = model.Cijena,
                        VrijediOd = model.VrijediOd,
                        VrijediDo = model.VrijediDo,
                        Datum = DateTime.Now
                    });

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Prikaz(int id)
        {
            var projekcija = _dbContext.Projekcija
                .Include(x => x.Sala)
                .Include(x => x.Film).ThenInclude(x => x.Zanr)
                .FirstOrDefault(x => x.Id == id);

            if (projekcija != null)
            {
                return View(new ProjekcijaPrikazVM
                {
                    Id = projekcija.Id,
                    Sala = projekcija.Sala.Naziv,
                    Cijena = projekcija.Cijena.ToString("N"),
                    Kreiran = projekcija.Datum.ToString("dd.MM.yyyy"),
                    VrijediOdDo = $"{projekcija.VrijediOd.ToString("dd.MM.yyyy")} - {projekcija.VrijediDo.ToString("dd.MM.yyyy")}",
                    Film = new FilmPrikazVM
                    {
                        Id = projekcija.Film.Id,
                        Naslov = projekcija.Film.Naslov,
                        Trajanje = projekcija.Film.Trajanje,
                        Zanr = projekcija.Film.Zanr.Naziv,
                        GodinaSnimanja = projekcija.Film.GodinaSnimanja,
                        Reditelj = projekcija.Film.Reditelj,
                        Sadrzaj = projekcija.Film.Sadrzaj,
                        VideoLink = projekcija.Film.VideoLink,
                        Plakat = CommonHelper.GetImageBase64(projekcija.Film.Plakat),
                        Glumci = projekcija.Film.Glumac
                    }
                });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult KupiKartu(int id)
        {
            var projekcija = _dbContext.Projekcija
                .Include(x => x.Sala)
                .Include(x => x.Film).ThenInclude(x => x.Zanr)
                .FirstOrDefault(x => x.Id == id);

            if (projekcija != null)
            {
                return View(new ProjekcijaKupiKartuVM
                {
                    Id = projekcija.Id,
                    Sala = projekcija.Sala.Naziv,
                    Cijena = projekcija.Cijena.ToString("N"),
                    Kreiran = projekcija.Datum.ToString("dd.MM.yyyy"),
                    VrijediOdDo = $"{projekcija.VrijediOd.ToString("dd.MM.yyyy")} - {projekcija.VrijediDo.ToString("dd.MM.yyyy")}",
                    Film = new FilmPrikazVM
                    {
                        Id = projekcija.Film.Id,
                        Naslov = projekcija.Film.Naslov,
                        Trajanje = projekcija.Film.Trajanje,
                        Zanr = projekcija.Film.Zanr.Naziv,
                        GodinaSnimanja = projekcija.Film.GodinaSnimanja,
                        Reditelj = projekcija.Film.Reditelj,
                        Sadrzaj = projekcija.Film.Sadrzaj,
                        VideoLink = projekcija.Film.VideoLink,
                        Plakat = CommonHelper.GetImageBase64(projekcija.Film.Plakat),
                        Glumci = projekcija.Film.Glumac
                    }
                });

            }
            else
            {
                return RedirectToAction("UcitajSveProjekcije");
            }
        }


        public IActionResult UcitajSveProjekcije()
        {
            var getAll = _dbContext.Projekcija
                .Include(x => x.Film).ThenInclude(x => x.Zanr)
                .Include(x => x.Sala);

            var projekcijaResult = getAll
                .Select(result => new ProjekcijaUcitajSveVM
                {
                    Id = result.Id,
                    VrijemeDo = result.VrijediDo.ToString("dd.MM.yyyy"),
                    Film = new FilmPrikazPocetnaVM
                    {
                        Id = result.Film.Id,
                        Naslov = result.Film.Naslov,
                        Trajanje = result.Film.Trajanje,
                        Zanr = result.Film.Zanr.Naziv,
                        GodinaSnimanja = result.Film.GodinaSnimanja,
                        Reditelj = result.Film.Reditelj,
                        Glumci = result.Film.Glumac,
                        Plakat = CommonHelper.GetImageBase64(result.Film.Plakat)
                    }

                }).ToList();

            return View(projekcijaResult);

            //var model = new ProjekcijaIndexModel()
            //{
            //    ProjekcijeAll = projekcijaResult
            //};

        }

        [HttpGet("UcitajProjekcijePoFilteru")]
        public async Task<IActionResult> UcitajProjekcijePoFilteru([FromQuery] string searching)
        {
            //return View(_dbContext.Projekcija.Where(x => x.Film.Naslov.Contains(searching) || searching == null).ToList());

            ViewData["CurrentFilter"] = searching;

            var filmNaziv = from x in _dbContext.Projekcija
                            select x;

            if (!string.IsNullOrEmpty(searching))
            {
                filmNaziv = filmNaziv.Where(x => x.Film.Naslov.ToLower().Trim().Replace(" ", "").Contains(searching.Trim().Replace(" ", "").ToLower()));
            }

            var response = await filmNaziv.Select(result => new ProjekcijaUcitajSveVM
            {
                Id = result.Id,
                VrijemeDo = result.VrijediDo.ToString("dd.MM.yyyy"),
                Film = new FilmPrikazPocetnaVM
                {
                    Id = result.Film.Id,
                    Naslov = result.Film.Naslov,
                    Trajanje = result.Film.Trajanje,
                    Zanr = result.Film.Zanr.Naziv,
                    GodinaSnimanja = result.Film.GodinaSnimanja,
                    Reditelj = result.Film.Reditelj,
                    Glumci = result.Film.Glumac,
                    Plakat = CommonHelper.GetImageBase64(result.Film.Plakat)
                }

            }).ToListAsync();
            return View("UcitajSveProjekcije", response);
        }

        [HttpGet("UcitajProjekcijePoZanru")]
        public async Task<IActionResult> UcitajProjekcijePoZanru(string id)
        {
            var filmZanr = from x in _dbContext.Projekcija
                           select x;

            //if(!string.IsNullOrEmpty(zanr))
            //{
            filmZanr = filmZanr.Where(x => x.Film.Zanr.Naziv.Contains(id));
            //}

            var response = await filmZanr.Select(result => new ProjekcijaUcitajSveVM
            {
                Id = result.Id,
                VrijemeDo = result.VrijediDo.ToString("dd.MM.yyyy"),
                Film = new FilmPrikazPocetnaVM
                {
                    Id = result.Film.Id,
                    Naslov = result.Film.Naslov,
                    Trajanje = result.Film.Trajanje,
                    Zanr = result.Film.Zanr.Naziv,
                    GodinaSnimanja = result.Film.GodinaSnimanja,
                    Reditelj = result.Film.Reditelj,
                    Glumci = result.Film.Glumac,
                    Plakat = CommonHelper.GetImageBase64(result.Film.Plakat)
                }

            }).ToListAsync();

            return View("UcitajSveProjekcije", response);
        }

        [HttpGet("UcitajProjekcijePoDanu")]
        public async Task<IActionResult> UcitajProjekcijePoDanu(string datum)
        {
            var pretragaDan = from x in _dbContext.Projekcija
                              select x;

            if (!string.IsNullOrEmpty(datum))
            {
                pretragaDan = pretragaDan.Where(x => x.VrijediDo.ToString().Contains(datum));
            }

            var response = await pretragaDan.Select(result => new ProjekcijaUcitajSveVM
            {
                Id = result.Id,
                VrijemeDo = result.VrijediDo.ToString("dd.MM.yyyy"),
                Film = new FilmPrikazPocetnaVM
                {
                    Id = result.Film.Id,
                    Naslov = result.Film.Naslov,
                    Trajanje = result.Film.Trajanje,
                    Zanr = result.Film.Zanr.Naziv,
                    GodinaSnimanja = result.Film.GodinaSnimanja,
                    Reditelj = result.Film.Reditelj,
                    Glumci = result.Film.Glumac,
                    Plakat = CommonHelper.GetImageBase64(result.Film.Plakat)
                }

            }).ToListAsync();

            return View("UcitajSveProjekcije", response);
        }

        public IActionResult Uredi(int id)
        {
            var projekcija = _dbContext.Projekcija.FirstOrDefault(x => x.Id == id);

            if (projekcija != null)
            {
                ViewData["Sale"] = _dbContext.Sala
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Naziv
                    });

                ViewData["Filmovi"] = _dbContext.Film
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Naslov
                    });

                return View(new ProjekcijaSnimiVM
                {
                    Id = projekcija.Id,
                    FilmId = projekcija.FilmId,
                    SalaId = projekcija.SalaId,
                    Cijena = projekcija.Cijena,
                    VrijediOd = projekcija.VrijediOd,
                    VrijediDo = projekcija.VrijediDo
                });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Uredi(ProjekcijaSnimiVM model, IFormFile file)
        {
            try
            {
                var projekcija = _dbContext.Projekcija.FirstOrDefault(x => x.Id == model.Id);

                if (projekcija != null)
                {
                    projekcija.FilmId = model.FilmId;
                    projekcija.SalaId = model.SalaId;
                    projekcija.Cijena = model.Cijena;
                    projekcija.VrijediOd = model.VrijediOd;
                    projekcija.VrijediDo = model.VrijediDo;

                    _dbContext.SaveChanges();
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
                var projekcija = _dbContext.Projekcija.FirstOrDefault(x => x.Id == id);

                if (projekcija != null)
                {
                    _dbContext.Projekcija.Remove(projekcija);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Index");
        }

        #region Methods
        private void LoadViewData(ProjekcijaSnimiVM model)
        {
            ViewData["Sale"] = _dbContext.Sala
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv,
                    Selected = (x.Id == model.SalaId)
                });

            ViewData["Filmovi"] = _dbContext.Film
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naslov,
                    Selected = (x.Id == model.FilmId)
                });
        }

        private bool VaildProjekcija(ProjekcijaSnimiVM models)
        {
            if (models.VrijediOd >= models.VrijediDo)
            {
                ModelState.AddModelError(nameof(models.VrijediOd), "Datum VrijediOd mora biti manji od datuma VrijediDo");
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
