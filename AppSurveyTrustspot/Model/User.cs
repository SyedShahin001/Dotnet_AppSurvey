using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppSurveyTrustspot.Model
{
	public class User
	{
		[Key]
		public int UserId { get; set; }

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
