using System.ComponentModel.DataAnnotations;

namespace AppSurveyTrustspot.Model
{
	public class Admin
	{
		[Key]
		public int AdminId { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]

		public string Password { get; set; }
	}
}
