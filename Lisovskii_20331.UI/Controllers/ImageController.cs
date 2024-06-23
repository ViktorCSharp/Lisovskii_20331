using Lisovskii_20331.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lisovskii_20331.UI.Controllers
{
	public class ImageController(UserManager<ApplicationUser>userManager) : Controller
	{
		public async Task<IActionResult> GetAvatar()
		{
			var email = User.FindFirst(ClaimTypes.Email)!.Value;
			var user = await userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return NotFound();
			}
			if (user.Avatar !=null && user.Avatar.Length != 0)
				return File(user.Avatar, user.MimeType);
			
			var imagePath = Path.Combine("Images", "user.png");
			return File(imagePath, "image/png");
		}
	}
}
