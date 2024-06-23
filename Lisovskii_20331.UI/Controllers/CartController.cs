using Humanizer.Localisation;
using Lisovskii_20331.UI.Data;
using Lisovskii_20331.UI.Services;
using Lsiovskii_20331.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lisovskii_20331.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookService _productService;
        private Cart _cart;
        public CartController(IBookService productService)
        {
            _productService = productService;

        }

        // GET: CartController 
        public ActionResult Index()
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            return View(_cart.CartItems);
        }

        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var data = await _productService.GetBookByIdAsync(id);
            var productResponse = await _productService.GetBookListAsync(null, 1);
            if (data.Success)
            {
                for(int j =1; j<=2; j++)
                {
                    productResponse = await _productService.GetBookListAsync(null, j);
                    for (int i = 0; i < productResponse.Data.Items.Count; i++)
                    {
                        Book book = productResponse.Data.Items[i];
                        if (book.Id == id)
                        {
                            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
                            _cart.AddToCart(book/*data.Data*/);
                            HttpContext.Session.Set<Cart>("cart", _cart);
                            break;
                        }
                    }
                }
            }
            return Redirect(returnUrl);
        }

        [Route("[controller]/remove/{id:int}")]
        public ActionResult Remove(int id)
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            _cart.RemoveItems(id);
            HttpContext.Session.Set<Cart>("cart", _cart);
            return RedirectToAction("index");
        }
    }
}
