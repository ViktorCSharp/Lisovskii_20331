using Lisovskii_20331.UI.Controllers;
using Lisovskii_20331.UI.Services;
using Lsiovskii_20331.Domain.Entities;
using Lsiovskii_20331.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisosvskii_20331.Tests
{
    public class ProductControllerTests
    {
        IBookService _productService;
        IGenreService _categoryService;
        public ProductControllerTests()
        {
            SetupData();
        }
        // Список категорий сохраняется во ViewData 
        [Fact]
        public async void IndexPutsCategoriesToViewData()
        {
            //arrange 
            var controller = new ProductController(_categoryService, _productService);

            //act 
            var response = await controller.Index(null);

            //assert 
            var view = Assert.IsType<ViewResult>(response);
            var categories = Assert.IsType<List<Genre>>(view.ViewData["genres"]);
            Assert.Equal(5, categories.Count);
            Assert.Equal("Все", view.ViewData["currentGenre"]);

        }
        // Имя текущей категории сохраняется во ViewData 
        [Fact]
        public async void IndexSetsCorrectCurrentCategory()
        {
            //arrange 
            var categories = await _categoryService.GetGenreListAsync();
            var currentCategory = categories.Data[0];
            var controller = new ProductController(_categoryService, _productService);

            //act 
            var response = await controller.Index(currentCategory.NormalizedName);

            //assert 
            var view = Assert.IsType<ViewResult>(response);

            Assert.Equal(currentCategory.Name, view.ViewData["currentGenre"]);
        }
        // В случае ошибки возвращается NotFoundObjectResult 
        [Fact]
        public async void IndexReturnsNotFound()
        {
            //arrange         
            string errorMessage = "Test error";
            var categoriesResponse = new ResponseData<List<Genre>>();
            categoriesResponse.Success = false;
            categoriesResponse.ErrorMessage = errorMessage;

            _categoryService.GetGenreListAsync().Returns(Task.FromResult(categoriesResponse))
            ;
            var controller = new ProductController(_categoryService, _productService);

            //act 
            var response = await controller.Index(null);

            //assert 
            var result = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(errorMessage, result.Value.ToString());
        }
        // Настройка имитации ICategoryService и IProductService 
        void SetupData()
        {
            _categoryService = Substitute.For<IGenreService>();
            var categoriesResponse = new ResponseData<List<Genre>>();
            categoriesResponse.Data = new List<Genre>
        {
            new Genre {Id=1, Name="Романы", NormalizedName="Romance"},
            new Genre {Id=2, Name="Драммы", NormalizedName="Dramаs"},
            new Genre {Id=3, Name="Детективы", NormalizedName="Detectives"},
            new Genre {Id=4, Name="Фентези", NormalizedName="Fantasys"},
            new Genre {Id=5, Name="Манга", NormalizedName="Manga"}
        };

            _categoryService.GetGenreListAsync().Returns(Task.FromResult(categoriesResponse))
            ;

            _productService = Substitute.For<IBookService>();

            var books = new List<Book>
        {
            new Book {Id = 1 },
            new Book { Id = 2 },
            new Book { Id = 3 },
            new Book { Id = 4 },
            new Book { Id = 5 }
        };

            var productResponse = new ResponseData<ListModel<Book>>();
            productResponse.Data = new ListModel<Book> { Items = books };
            _productService.GetBookListAsync(Arg.Any<string?>(), Arg.Any<int>()).Returns(productResponse);
        }
    }
}
