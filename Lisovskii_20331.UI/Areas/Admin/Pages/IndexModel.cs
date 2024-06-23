﻿using Lisovskii_20331.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lsiovskii_20331.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Lisovskii_20331.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel 
    {

        private readonly IBookService _bookService;

        public IndexModel(IBookService productService)
        {
            //_context = context; 
            _bookService = productService;
        }

        public List<Book> Book { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public async Task OnGetAsync(int? pageNo = 1)
        {
            var response = await _bookService.GetBookListAsync(null, pageNo.Value);
            if (response.Success)
            {
                Book = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}
