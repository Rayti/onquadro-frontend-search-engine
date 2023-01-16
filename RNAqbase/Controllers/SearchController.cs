﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RNAqbase.BackEnd;
using RNAqbase.Models.Search;
using RNAqbase.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNAqbase.Controllers
{
	[Route("api/[controller]")]
	public class SearchController : Controller
	{
		private readonly ISearchService searchService;

		public SearchController(ISearchService searchService)
		{
			this.searchService = searchService;
		}

		[HttpPost("[action]")]
		public ActionResult PostFilters()
		{
			Request.EnableBuffering();
			Request.Body.Position = 0;
			string rawRequestBody = new StreamReader(Request.Body).ReadToEnd();

			try
            {
				Filter.Filters = JsonConvert.DeserializeObject<List<Filter>>(rawRequestBody, new JsonFilterConverter());
            }
            catch
            {
				Filter.Filters = null;
				return BadRequest();
            }

			return Ok();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetResults()
		{
			return Ok(await searchService.GetAllResults());
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetExperimentalMethod()
		{
			return Ok(await searchService.GetExperimentalMethod());
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetONZ()
		{
			return Ok(await searchService.GetONZ());
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetIons()
		{
			return Ok(await searchService.GetIons());
		}
    
		[HttpGet("[action]")]
		public async Task<IActionResult> GetMoleculeType()
		{
			return Ok(await searchService.GetMoleculeType());
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetWebbaDaSilva()
		{
			return Ok(await searchService.GetWebbaDaSilva());
		}
	}
}