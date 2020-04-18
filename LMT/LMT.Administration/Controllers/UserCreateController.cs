using System.Threading.Tasks;
using LMT.Common.IServices;
using LMT.Common.ResponsiveModels;
using LMT.Common.Validators;
using LMT.Controllers;
using LMT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace LMT.Administration.Controllers
{
    [Route("[controller]")]
   
    public class UserCreateController : GlobalLMTController
	{

		private readonly IUserCreateService  _userCreateService;
		ResponseData objResponse = new ResponseData();
		Status status = new Status();
		Pagination _pagination = new Pagination();

		public UserCreateController(IUserCreateService userCreateService)
		{
			_userCreateService = userCreateService;
		}




		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromBody] UserCreate userCreate)
		{
			var validator = new CreateUpdateValidator();
			ValidationResult results = validator.Validate(userCreate);
			if (!results.IsValid)
			{
				return CreateResponseWithStatus(StatusCodes.Status400BadRequest, GetValidationErrors(results.Errors));
			}
			status = await _userCreateService.CreateAsync(userCreate);
			return CreateResponseWithStatus(status.Code, status);
		}

	}
}