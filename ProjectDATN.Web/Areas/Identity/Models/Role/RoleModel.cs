using Microsoft.AspNetCore.Identity;

namespace ProjectDATN.Web.Areas.Identity.Models.Role
{
	public class RoleModel : IdentityRole
	{
		public string[] Claims { get; set; }
	}
}