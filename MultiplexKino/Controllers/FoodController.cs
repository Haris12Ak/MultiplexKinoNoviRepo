using Microsoft.AspNetCore.Mvc;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Areas.Identity.Data.EntityModels;
using MultiplexKino.Helpers;
using MultiplexKino.Models.FoodModels;
using MultiplexKino.Services;

namespace MultiplexKino.Controllers
{
    public class FoodController : Controller
    {
        private readonly MultiplexKinoDbContext _db;
        private IEmailLogSender _emailLogSender;

        public FoodController(MultiplexKinoDbContext db, IEmailLogSender emailLogSender)
        {
            _db = db;
            _emailLogSender = emailLogSender;
        }

        public IActionResult Index()
        {
            var food = _db.Food
                .Select(x => new FoodListaVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Price = x.Price.ToString("N"),
                    Image = CommonHelper.GetImageBase64(x.Image),
                });

            return View(food);
        }

        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(FoodSnimiVM model, IFormFile file)
        {
            try
            {
                _db.Food.Add(new Food
                {
                    Name = model.Name,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    Image = CommonHelper.GetImageByteArray(file)
                });

                _db.SaveChanges();
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
                var food = _db.Food.FirstOrDefault(x => x.Id == id);

                if(food != null)
                {
                    _db.Food.Remove(food);
                    _db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _emailLogSender.SendEmailLog(HttpContext, ex);
            }

            return RedirectToAction("Index");
        }
    }
}
