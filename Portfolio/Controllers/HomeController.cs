using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Misc.Services.EmailService;
using System.Diagnostics;

namespace Portfolio.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationContext db;
		private readonly ILogger<HomeController> _logger;
		private readonly IEmailService _emailService;
		public HomeController(ILogger<HomeController> logger, IEmailService email, ApplicationContext context)
		{
			db = context;
			_logger = logger;
			_emailService = email;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> PostEmail(EmailViewModel model)
		{

			var message = new Message(new string[] { model.Email }, ":(", model.Message);
			db.Messages?.Add(new EmailViewModel()
			{
				Id = Guid.NewGuid(),
				Email = model.Email,
				Message = model.Message
			});
			db.SaveChanges();
			await _emailService.SendEmailAsync(message);
			return Ok();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}