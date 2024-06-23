
using System.Text.Json.Serialization;


namespace Lsiovskii_20331.Domain.Entities
{
	public class Book
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Pages { get; set; }
		public string? Image { get; set; }

		// Навигационные свойства 
		public int GenreId { get; set; }
		//[JsonIgnore]
		public Genre? Genre { get; set; }
	}
}
