using Lsiovskii_20331.Domain.Entities;
using Lsiovskii_20331.Domain.Models;

namespace Lisovskii_20331.UI.Services
{
    public class ApiGenreService(HttpClient httpClient) : IGenreService
    {
        public async Task<ResponseData<List<Genre>>> GetGenreListAsync()
        {
            var result = await httpClient.GetAsync(httpClient.BaseAddress);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<List<Genre>>>();
            };

            var response = new ResponseData<List<Genre>>
            { Success = false, ErrorMessage = "Ошибка чтения API" };
            return response;
        }
    }
}
