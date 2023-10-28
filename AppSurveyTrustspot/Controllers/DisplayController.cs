using AppSurveyTrustspot.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DisplaysSurvey.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DisplayController : ControllerBase
	{
		private readonly TrustspotContext repo;

		private readonly ILogger<DisplayController> logger;
	public DisplayController(TrustspotContext repo, ILogger<DisplayController> logger)//dependency injection
		{
			this.repo = repo;
			this.logger = logger;
		}


		//Most reviewed app
		[HttpGet("MostReviewedApp")]
		public IActionResult MostReviewedApp()
		{
			List<Review> reviews = repo.Reviews.ToList();
			List<App> app = repo.Apps.ToList();
			var s = (from r in reviews
					join a in app on r.AppId equals a.AppId
					group a by a.AppName into g
					orderby g.Count() descending
					select new { AppName = g.Key, number_of_reviews = g.Count() })
			        .FirstOrDefault();
			logger.LogInformation("This is from Display Controller");
			return StatusCode(200, s);
		}

		//Review by email
		[HttpGet("AppNameAndReviewByEmail/{email}")]
		public IActionResult AppNameAndReviewByEmail(string email)
		{
			var appReviews = (from r in repo.Reviews
							  join a in repo.Apps on r.AppId equals a.AppId
							  where r.Email == email
							  select new
							  {
								  AppName = a.AppName,
								  ReviewContent = r.ReviewText
							  }).ToList();

			return Ok(appReviews);
		}

		//Review by appname
		[HttpGet("AppNameAndReview/{appName}")]
		public IActionResult AppNameAndReview(string appName)
		{
			var appReviews = (from r in repo.Reviews
							  join a in repo.Apps on r.AppId equals a.AppId
							  where a.AppName == appName
							  select new
							  {
								  AppName = a.AppName,
								  ReviewContent = r.ReviewText
							  }).ToList();

			return Ok(appReviews);
		}

        //Display the app name and number of reviews of each app
        [HttpGet("AppNameandItsReviews")]
        public IActionResult AppNameandItsReviews()
        {
            List<Review> reviews = repo.Reviews.ToList();
            List<App> app = repo.Apps.ToList();
            var s = from r in reviews
                    join a in app on r.AppId equals a.AppId
                    group a by a.AppName into g
                    orderby g.Count() descending
                    select new { AppName = g.Key, number_of_reviews = g.Count() };
            return StatusCode(200, s);
        }

    }
}
	
