using Microsoft.AspNetCore.Mvc;

namespace Eshop.Web.Components
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
