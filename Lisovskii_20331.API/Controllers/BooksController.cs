using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lisovskii_20331.API.Data;
using Lsiovskii_20331.Domain.Entities;
using Lsiovskii_20331.Domain.Models;

namespace Lisovskii_20331.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BooksController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Book>>>> GetBooks(
                        string? genre,
                        int pageNo = 1,
                        int pageSize = 3)
        {

            // Создать объект результата 
            var result = new ResponseData<ListModel<Book>>();
            // Фильтрация по категории загрузка данных категории 
            var data = _context.Books
                .Include(d => d.Genre)
                .Where(d => String.IsNullOrEmpty(genre)
                        || d.Genre.NormalizedName.Equals(genre));

            // Подсчет общего количества страниц 
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
            if (pageNo > totalPages)
                pageNo = totalPages;

            // Создание объекта ProductListModel с нужной страницей данных 
            var listData = new ListModel<Book>()
            {
                Items = await data
                                .Skip((pageNo - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата 
            result.Data = listData;

            // Если список пустой 
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }

            return result;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(int id, IFormFile image)
        {
            // Найти объект по Id 
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Путь к папке wwwroot/Images 
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // получить случайное имя файла 
            var randomName = Path.GetRandomFileName();
            // получить расширение в исходном файле 
            var extension = Path.GetExtension(image.FileName);
            // задать в новом имени расширение как в исходном файле 
            var fileName = Path.ChangeExtension(randomName, extension);
            // полный путь к файлу 
            var filePath = Path.Combine(imagesPath, fileName);
            // создать файл и открыть поток для записи 
            using var stream = System.IO.File.OpenWrite(filePath);
            // скопировать файл в поток 
            await image.CopyToAsync(stream);
            // получить Url хоста 
            var host = "https://" + Request.Host;
            // Url файла изображения 
            var url = $"{host}/Images/{fileName}";
            // Сохранить url файла в объекте 
            book.Image = url;
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

    }
}
