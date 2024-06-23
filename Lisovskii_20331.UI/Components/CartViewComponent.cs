using Lisovskii_20331.UI.Data;
using Lsiovskii_20331.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Lisovskii_20331.UI.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<Cart>("cart");
            return View(cart);
        }
    }
}
