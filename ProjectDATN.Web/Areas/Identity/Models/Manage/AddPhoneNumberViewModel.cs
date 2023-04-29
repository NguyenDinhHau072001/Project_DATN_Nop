using System.ComponentModel.DataAnnotations;

namespace ProjectDATN.Web.Areas.Identity.Models.Manage
{
	public class AddPhoneNumberViewModel
	{
		[Required]
		[Phone]
		[Display(Name = "Số điện thoại")]
		public string PhoneNumber { get; set; }
	}
}