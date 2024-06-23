using Lsiovskii_20331.Domain.Entities;
using Lsiovskii_20331.Domain.Models;

namespace Lisovskii_20331.UI.Services
{
	public interface IGenreService
	{
		public Task<ResponseData<List<Genre>>> GetGenreListAsync();
	}
}