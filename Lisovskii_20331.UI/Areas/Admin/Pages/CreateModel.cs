using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Lisovskii_20331.UI.Services;
using Lsiovskii_20331.Domain.Entities;

namespace Lisovskii_20331.UI.Areas.Admin.Pages
{
    public class CreateModel(IGenreService genreService, IBookService bookService) : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            var genreListData = await genreService.GetGenreListAsync();
            ViewData["GenreId"] = new SelectList(genreListData.Data, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await bookService.CreateBookAsync(Book, Image);

            return RedirectToPage("./Index");
        }
    }
}
