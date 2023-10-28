using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppSurveyTrustspot.Model;



#nullable disable

namespace AppSurveyTrustspot.Model
{
	public class Review
	{
		[Key]
		public int ReviewId { get; set; }
		public int AppId { get; set; }

		[ForeignKey("AppId")]
		public App App { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string ReviewText { get; set; }

	}
}
