using Microsoft.AspNetCore.Identity;
using ProjectDATN.Data.Entities;
using System.ComponentModel;

namespace ProjectDATN.Web.Areas.Identity.Models.User
{
	public class AddUserRoleModel
	{
		public AppUser user { get; set; }

		[DisplayName("Các role gán cho user")]
		public string[] RoleNames { get; set; }

		public List<IdentityRoleClaim<string>> claimsInRole { get; set; }
		public List<IdentityUserClaim<string>> claimsInUserClaim { get; set; }
	}
}