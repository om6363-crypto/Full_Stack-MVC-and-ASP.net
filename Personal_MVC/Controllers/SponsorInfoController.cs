using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal_MVC.Models;
using System.Net.Http;
using System.Text;

namespace Personal_MVC.Controllers
{
	public class SponsorInfoController : Controller
	{
		Uri baseAddress = new Uri("http://localhost:5254/api/Sponsor");
		private readonly HttpClient _client;

		public SponsorInfoController()
		{
			_client = new HttpClient();
			_client.BaseAddress = baseAddress;
		}

		[HttpGet]
        public IActionResult GetSponsorDetails()
        {
            List<SponsorDetails> sponsorDetailsList = new List<SponsorDetails>();
            HttpResponseMessage response = _client.GetAsync("http://localhost:5254/api/Sponsor/Sponsor-Details").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                sponsorDetailsList = JsonConvert.DeserializeObject<List<SponsorDetails>>(data);
            }
            return View(sponsorDetailsList);
        }

        [HttpGet]
		public IActionResult GetMatchDetails()
		{
			List<MatchDetails> matchDetailsList = new List<MatchDetails>();
			HttpResponseMessage response = _client.GetAsync("http://localhost:5254/api/Sponsor/Match-Details").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				matchDetailsList = JsonConvert.DeserializeObject<List<MatchDetails>>(data);
			}

			return View(matchDetailsList);
		}

		[HttpGet]
		public IActionResult GetSponsorsMatches(int year)
		{
			List<SponsorMatches> sponsorMatchesList = new List<SponsorMatches>();
			HttpResponseMessage response = _client.GetAsync($"http://localhost:5254/api/Sponsor/sponsors-matches?year={2024}").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				sponsorMatchesList = JsonConvert.DeserializeObject<List<SponsorMatches>>(data);
			}

			return View(sponsorMatchesList);
		}

		[HttpGet]
		public IActionResult AddPayment()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddPayment(Payments payment)
		{
			if (ModelState.IsValid)
			{
				var jsonContent = JsonConvert.SerializeObject(payment);
				var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await _client.PostAsync("http://localhost:5254/api/Sponsor/Add-Payments", contentString);

				if (response.IsSuccessStatusCode)
				{
					TempData["SuccessMessage"] = "Payment added successfully.";
					return RedirectToAction("GetSponsorsMatches");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "An error occurred while adding the payment.");
				}
			}

			return View(payment);
		}
	}
}
