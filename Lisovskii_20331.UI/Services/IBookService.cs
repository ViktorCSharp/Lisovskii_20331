using Lsiovskii_20331.Domain.Entities;
using Lsiovskii_20331.Domain.Models;

namespace Lisovskii_20331.UI.Services
{
	public interface IBookService
	{
		/// <summary> 
		/// Получение списка всех объектов 
		/// </summary> 
		/// <param name="genreNormalizedName">нормализованное имя категории для фильтрации</param>
	/// <param name="pageNo">номер страницы списка</param> 
	/// <returns></returns> 
	public Task<ResponseData<ListModel<Book>>> GetBookListAsync(string? genreNormalizedName, int pageNo = 1);

		/// <summary> 
		/// Поиск объекта по Id 
		/// </summary> 
		/// <param name="id">Идентификатор объекта</param> 
		/// <returns>Найденный объект или null, если объект не найден</returns> 
		public Task<ResponseData<Book>> GetBookByIdAsync(int id);
        /// <summary> 
        /// Обновление объекта 
        /// </summary> 
        /// <param name="id">Id изменяемомго объекта</param> 
        /// <param name="product">объект с новыми параметрами</param> 
        /// <param name="formFile">Файл изображения</param> 
        /// <returns></returns> 
        public Task UpdateBookAsync(int id, Book product, IFormFile? formFile);
		/// <summary> 
		/// Удаление объекта 
		/// </summary> 
		/// <param name="id">Id удаляемомго объекта</param> 
		/// <returns></returns> 
		public Task DeleteBookAsync(int id);
		/// <summary> 
		/// Создание объекта 
		/// </summary> 
		/// <param name="product">Новый объект</param> 
		/// <param name="formFile">Файл изображения</param> 
		/// <returns>Созданный объект</returns> 
		public Task<ResponseData<Book>> CreateBookAsync(Book product, IFormFile? formFile);
	}
}
