using Lisovskii_20331.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lisovskii_20331.UI.Controllers
{
	public class ProductController(IGenreService genreService, IBookService productService) : Controller
	{
        [Route("Catalog")]
        [Route("Catalog/{genre}")]
        public async Task<IActionResult> Index(string? genre, int pageNo = 1)
		{
            // получить список жанров
            var genreResponse = await genreService.GetGenreListAsync();

            // если список не получен, вернуть код 404 
            if (!genreResponse.Success)
                return NotFound(genreResponse.ErrorMessage);

            // передать список жанров во ViewData        
            ViewData["genres"] = genreResponse.Data;

            // передать во ViewData имя текущего жанра 
            var currentGenre = genre == null ? "Все" : genreResponse.Data.FirstOrDefault(c =>c.NormalizedName == genre)?.Name;

            ViewData["currentGenre"] = currentGenre;

            var productResponse = await productService.GetBookListAsync(genre, pageNo);
			if (!productResponse.Success)
				return NotFound(productResponse.ErrorMessage);
			return View(productResponse.Data);
		}

	}
}
