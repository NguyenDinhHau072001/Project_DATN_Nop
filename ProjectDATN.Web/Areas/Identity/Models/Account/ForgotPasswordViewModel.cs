﻿using System.ComponentModel.DataAnnotations;

namespace ProjectDATN.Web.Areas.Identity.Models.Account
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}