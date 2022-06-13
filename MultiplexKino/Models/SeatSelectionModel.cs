using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Areas.Identity.Data.EntityModels;

namespace MultiplexKino.Models
{
    public class SeatSelectionModel : PageModel
    {
        private readonly MultiplexKinoDbContext _db;

        public SeatSelectionModel(MultiplexKinoDbContext db)
        {
            _db = db;
        }

        public MoviesForShowingIndexModel MoviesForShowingData { get; set; }
        public IEnumerable<Food> FoodChose { get; set; }
        public int MoviesId { get; set; }

        [BindProperty]
        public List<int> SeatsChecked { get; set; }
        public async Task OnGetAsync(int? id)
        {
            FoodChose = await _db.Food.ToListAsync();

            MoviesForShowingData = new MoviesForShowingIndexModel();
            MoviesForShowingData.MoviesForShowing = await _db.MoviesForShowing
                .Include(i => i.Movie)
                .Include(i => i.CinemaHall)
                .ThenInclude(a => a.SeatsForHall)
                .ThenInclude(z => z.Seats)
                .ToListAsync();

            if (id != null)
                MoviesId = (int)id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var s in SeatsChecked)
            {
                var seat = await _db.Seats.FindAsync(s);
                if (seat == null)
                    return NotFound();
                if (!seat.IsBusy)
                    seat.IsBusy = true;

                _db.Seats.Update(seat);
                await _db.SaveChangesAsync();
            }
            SeatsChecked.Clear();
            return RedirectToAction("SeatSelection", MoviesForShowingData);
        }
    }
}
