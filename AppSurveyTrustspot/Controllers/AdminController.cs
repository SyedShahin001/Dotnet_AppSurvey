using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AppSurveyTrustspot.Model;


[Route("api/[controller]")]
[ApiController]

public class AdminController : ControllerBase
{
	private readonly TrustspotContext _context;

	public AdminController(TrustspotContext context)
	{
		_context = context;
	}


	//Get all admins
	[HttpGet("GetAllAdmins")]
	public IActionResult GetAllUsers()
	{

		List<Admin> admins = _context.Admins.ToList();
		if (admins.Count == 0)
		{

			throw new System.Exception("Data not available");
		}
		else
		{
			return StatusCode(200, admins);
		}

	}
	//Admin Registration
	[HttpPost]
	[Route("api/admin/register")]
	public async Task<IActionResult> RegisterAdmin(Admin admin)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		try
		{
			// Check if the email address is already registered.
			if (_context.Admins.Any(a => a.EmailAddress == admin.EmailAddress))
			{
				return BadRequest("Email address is already registered.");
			}

			// Hash the password (you should use a secure password hashing library).
			string hashedPassword = HashPassword(admin.Password);

			// Create a new admin entity.
			var newAdmin = new Admin
			{
				FirstName = admin.FirstName,
				LastName = admin.LastName,
				EmailAddress = admin.EmailAddress,
				Password = hashedPassword
			};

			// Add the admin to the database.
			_context.Admins.Add(newAdmin);
			await _context.SaveChangesAsync();

			return Ok("Admin registered successfully.");
		}
		catch (Exception ex)
		{
			// Log the exception.
			// You can use a logging framework like Serilog, NLog, etc.
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	// Replace this with a secure password hashing method.
	//private string HashPassword(string password)
	//{
	//	// Implement a secure password hashing method here.
	//	string hashedPassword = HashPassword(admin.Password);
	//	// You should never store plain text passwords.
	//	// Consider using a library like BCrypt, Argon2, or PBKDF2.
	//	return password;
	//}

//	protected override void Dispose(bool disposing)
//	{
//		if (disposing)
//		{
//			_context.Dispose();
//		}
//		base.Dispose(disposing);
//	}
//}

	//[HttpPost("register")]

	//public async Task<IActionResult> RegisterAdmin([FromBody] Admin admin)
	//{
	//	if (admin == null)
	//	{
	//		return BadRequest("Invalid admin data.");
	//	}

	//	try
	//	{
	//		//Check if the email is already registered
	//		if (await _context.Admins.AnyAsync(a => a.EmailAddress == admin.EmailAddress))
	//		{
	//			return BadRequest("Email already registered.");
	//		}

	//		//You may want to hash user's password before saving
	//		admin.Password = HashPassword(admin.Password);

	//		_context.Admins.Add(admin);
	//		await _context.SaveChangesAsync();

	//		return Ok("Admin registered successfully.");
	//	}

	//	catch (Exception ex)
	//	{
	//		return StatusCode(500, $"Internal server error: {ex.Message}");
	//	}
	//}
	// Admin Login
	[HttpPost("login")]
	public async Task<IActionResult> LoginAdmin([FromBody] AdminLogin loginModel)
	{
		if (loginModel == null)
		{
			return BadRequest("Invalid login data.");
		}

		try
		{
			var admin = await _context.Admins.FirstOrDefaultAsync(a => a.EmailAddress == loginModel.EmailAddress);

			if (admin == null)
			{
				return NotFound("Admin not found.");
			}

			// You should verify the hashed password here
			if (!VerifyPassword(loginModel.Password, admin.Password))
			{
				return BadRequest("Invalid password.");
			}

			// Password is valid, you can generate a token or set up a session here
			// For simplicity, let's assume successful login
			return Ok("User logged in successfully.");
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	[HttpPost("logout")]
	public async Task<IActionResult> Logout()
	{
		try
		{
			// Perform any necessary actions to log the user out.
			// If you are using JWT token-based authentication, you can revoke the token here.
			// If you are using cookie-based authentication, you can sign the user out here.

			// Assuming a successful logout, return a 200 OK response.
			return Ok("Logout successful.");
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}

	// Replace these methods with your actual password hashing and verification logic
	private string HashPassword(string password)
	{
		// Implement your password hashing logic here.
		// You should use a secure hashing algorithm like BCrypt or Identity's PasswordHasher.
		// For example, using BCrypt:
		// return BCrypt.Net.BCrypt.HashPassword(password);
		// For this example, we'll use a simple hashing approach (not secure for production).
		return password;
	}

	private bool VerifyPassword(string inputPassword, string hashedPassword)
	{
		// Implement your password verification logic here.
		// You should use the same hashing algorithm you used for hashing the password.
		// For example, using BCrypt:
		// return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
		// For this example, we'll use a simple string comparison (not secure for production).
		return inputPassword == hashedPassword;
	}
}

