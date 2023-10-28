using AppSurveyTrustspot.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AppSurvey.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly TrustspotContext repo;
		//logging
		private readonly ILogger<ReviewController> logger;

		public ReviewController(TrustspotContext repo, ILogger<ReviewController> logger)//dependency injection
		{
			this.repo = repo;
			this.logger = logger;
		}


		// get all the reviews
		[HttpGet("FetchAllReviews")]
		public IActionResult FetchAllReviews()
		{
			List<Review> reviews = repo.Reviews.Include(r => r.App).ToList();
			if (reviews.Count == 0)
			{
				return StatusCode(404, reviews);
			}
			else
			{
				var reviewDetails = reviews.Select(r => new
				{
					ReviewId = r.ReviewId,
					AppName = r.App.AppName,
					AppId = r.AppId,
					Email = r.Email,
					ReviewText = r.ReviewText
				});
				logger.LogInformation("This is from Review Controller");
				return StatusCode(200, reviewDetails);
			}
		}


		//To post a new review
		[HttpPost("AddReview")]
		public IActionResult AddReview(Review rev)
		{
			if (rev == null)
			{
				return BadRequest("Invalid data: Review object is null");
			}
			try
			{
				repo.Reviews.Add(rev);
				repo.SaveChanges();
				return Created("New review is added", rev);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An error occurred while adding the review.");
			}
		}

		////To post a new review
		//[HttpPost("AddReview")]
		//public async Task<IActionResult> AddReview([FromBody] Review rev)
		//{
		//	if(rev==null)
		//	{
		//		return BadRequest("Invalid data: Review object is null.");
		//	}
		//	try
		//	{
		//		repo.Reviews.Add(rev);
		//		await repo.SaveChangesAsync();

		//		return Created("New review is added", rev);
		//	}
		//	catch (Exception ex)
		//	{
		//		return StatusCode(500, "An error occurred while adding the review.");
		//	}
		//	//rev.App = repo.Apps.Find(rev.AppId);
		//repo.Reviews.Add(rev);
			//repo.SaveChanges();
			//logger.LogInformation("This is from Review Controller - AddReview");
			//return Created("New review is Addeded", rev);

		//}


		/* [HttpPost("AddReviewName")]
		 public IActionResult AddReviewName(Review rev)
		 {
			 var app = repo.Apps.FirstOrDefault(a => a.AppName == rev.AppName);

			 if (app == null)
			 {
				 return NotFound("App not found with the specified name.");
			 }

			 rev.App = app;
			 repo.Reviews.Add(rev);
			 repo.SaveChanges();

			 return Created("New review is added", rev);
		 }*/

		[HttpPost("AddReviewAppId")]
		public IActionResult AddReviewAppId(Review rev)
		{
			var app = repo.Apps.FirstOrDefault(a => a.AppId == rev.AppId);

			if (app == null)
			{
				return NotFound("App not found with the specified AppId.");
			}

			rev.App = app;
			repo.Reviews.Add(rev);
			repo.SaveChanges();

			return Created("New review is added", rev);
		}
	}
}