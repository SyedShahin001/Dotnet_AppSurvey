﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppSurveyTrustspot.Model
{
	[NotMapped]
	public class AdminLogin
	{
		[Required]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]

		public string Password { get; set; }
	}
}
