using Microsoft.AspNetCore.Mvc.Rendering;
using MultiplexKino.Areas.Identity.Data;

namespace MultiplexKino.Core.ViewModels
{
    public class EditUserViewModel
    {
        public MultiplexKinoUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}
