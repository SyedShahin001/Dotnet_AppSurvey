//ORIGINAL

using AppSurveyTrustspot.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrustspotAppSurvery.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace TrustspotAppSurvery.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppController : ControllerBase
	{
		private readonly TrustspotContext repo;
		//Logging
		private readonly ILogger<AppController> logger;
		public AppController(TrustspotContext repo, ILogger<AppController> logger)//dependency injection
		{
			this.repo = repo;
			this.logger = logger;
		}

		//get all the apps
		[HttpGet("GetAllApps")]
		public IActionResult GetAllApps()
		{
			List<App> apps = repo.Apps.ToList();
			if (apps.Count == 0)
			{
				return StatusCode(204, apps);
			}
			else
			{
				//logging
				logger.LogInformation("This is from App Controller");
				return StatusCode(200, apps);
			}

		}
		//get the app by giving id of the app
		[HttpGet("GetApps/{AppId}")]
		public IActionResult GetApps(int AppId)
		{
			List<App> apps = repo.Apps.ToList();
			App app = apps.Find(a => a.AppId == AppId);
			if (app == null)
			{
				return StatusCode(404, "appid is not available");
			}
			else
			{
				return StatusCode(200, app);
			}
		}
		/*//add a new app
		[HttpPost("AddApp")]
		public async Task<IActionResult> PostApp([FromBody] App app)
		{
			if (app == null)
			{
				return StatusCode(400, "Invalid data: App object is null.");
			}
			repo.Apps.Add(app);
			try
			{
				await repo.SaveChangesAsync();
				return Ok(app);
			}
			catch (DbUpdateException)
			{
				return StatusCode(500, "An error occurred while saving the App");
			}
		}*/
		
        //add a new app
        [HttpPost("AddApp")]
        public IActionResult AddApp([FromBody] App app)
        {
            repo.Apps.Add(app);
            repo.SaveChanges();
            return Created(" New APP addeded", app);
        }


        //delete app the app
        [HttpDelete("{AppId}")]
		public IActionResult Delete(int AppId)
		{
			App id = repo.Apps.Find(AppId);
			if (id == null)
			{
				return StatusCode(404, "App Not Found");
			}
			else
			{
				repo.Apps.Remove(id);
				repo.SaveChangesAsync();
				return Ok("Deleted Successfully");
			}

		}
	}
}