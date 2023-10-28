using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppSurveyTrustspot.Model;



#nullable disable

namespace AppSurveyTrustspot.Model
{
	public class App
	{
		[Key]
		public int AppId { get; set; }

		[Required]
		public string? AppName { get; set; }
		public string? AppDescription { get; set; }

	}
}
