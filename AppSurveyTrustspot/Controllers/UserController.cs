using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AppSurveyTrustspot.Model;


[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly TrustspotContext _context;

    public UserController(TrustspotContext context)
    {
        _context = context;
    }


    //Get all users
    [HttpGet("GetAllUsers")]
    public IActionResult GetAllUsers()
    {

        List<User> users = _context.Users.ToList();
        if (users.Count == 0)
        {

            throw new System.Exception("Data not available");
        }
        else
        {
            return StatusCode(200, users);
        }

    }
    //User Registration
    [HttpPost]
    [Route("api/user/register")]
    public async Task<IActionResult> RegisterAdmin(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Check if the email address is already registered.
            if (_context.Users.Any(a => a.EmailAddress == user.EmailAddress))
            {
                return BadRequest("Email address is already registered.");
            }

            // Hash the password (you should use a secure password hashing library).
            string hashedPassword = HashPassword(user.Password);

            // Create a new admin entity.
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = hashedPassword
            };

            // Add the admin to the database.
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            // Log the exception.
            // You can use a logging framework like Serilog, NLog, etc.
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    //public async Task<IActionResult> RegisterUser([FromBody] User user)
    //{
    //	if(user == null)
    //	{
    //		return BadRequest("Invalid user data.");
    //	}

    //	try
    //	{
    //		//Check if the email is already registered
    //		if(await _context.Users.AnyAsync(a => a.EmailAddress == user.EmailAddress))
    //		{
    //			return BadRequest("Email already registered.");
    //		}

    //		//You may want to hash user's password before saving
    //		user.Password = HashPassword(user.Password);

    //		_context.Users.Add(user);
    //		await _context.SaveChangesAsync();

    //		return Ok("User registered successfully.");
    //	}

    //	catch (Exception ex)
    //	{
    //		return StatusCode(500, $"Internal server error: {ex.Message}");
    //	}
    //}
    // User Login
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLogin loginModel)
    {
        if (loginModel == null)
        {
            return BadRequest("Invalid login data.");
        }

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.EmailAddress == loginModel.EmailAddress);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // You should verify the hashed password here
            if (!VerifyPassword(loginModel.Password, user.Password))
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

