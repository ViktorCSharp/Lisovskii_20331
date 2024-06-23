using Lsiovskii_20331.Domain.Entities;
using Lsiovskii_20331.Domain.Models;

namespace Lisovskii_20331.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта 
            var uri = "https://localhost:7002/";
            // Получение контекста БД 
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Заполнение данными 
            if (!context.Genres.Any() && !context.Books.Any())
            {
                var genres = new Genre[]
                {
                    new Genre {Name="Романы", NormalizedName="Romance"},
                    new Genre {Name="Драммы", NormalizedName="Dramаs"},
                    new Genre {Name="Детективы", NormalizedName="Detectives"},
                    new Genre {Name="Фентези", NormalizedName="Fantasys"},
                    new Genre {Name="Манга", NormalizedName="Manga"}
                };
                await context.Genres.AddRangeAsync(genres);
                await context.SaveChangesAsync();

                var books = new List<Book>
            {
                new Book {
                    Name="Ведьмак",
                    Description="Баллады из психушки",
                    Pages =331,
                    Image="https://localhost:7002/Images/Witcher.jpg",
                    GenreId= genres.FirstOrDefault(c=>c.NormalizedName.Equals("Fantasys")).Id},

                new Book {
                    Name="Вавилон",
                    Description="Жизнь после 30",
                    Pages =420,
                    Image="https://localhost:7002/Images/Vavilon.jpg",
                    GenreId= genres.FirstOrDefault(c=>c.NormalizedName.Equals("Romance")).Id},

                new Book {
                    Name="Властелин колец",
                    Description="История о курьере",
                    Pages =1710,
                    Image="https://localhost:7002/Images/LordRing.jpg",
                    GenreId= genres.FirstOrDefault(c=>c.NormalizedName.Equals("Fantasys")).Id},

                new Book {
                    Name="Убийство в Восточном экспрессе",
                    Description="Сказ Витали о его поездке в Сочи",
                    Pages =400,
                    Image="https://localhost:7002/Images/Kill.jpg",
                    GenreId= genres.FirstOrDefault(c=>c.NormalizedName.Equals("Detectives")).Id},

                new Book {
                    Name="Горе от ума",
                    Description="Скорее от его отсутствия",
                    Pages =450,
                    Image="https://localhost:7002/Images/Gore.jpg",
                    GenreId= genres.FirstOrDefault(c=>c.NormalizedName.Equals("Dramаs")).Id},
            };
                await context.AddRangeAsync(books);
                await context.SaveChangesAsync();

            }
        }
    }
}
