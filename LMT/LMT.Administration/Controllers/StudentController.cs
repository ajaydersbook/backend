using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Threading.Tasks;
using LMT.IServices;
using LMT.Models;
using LMT.ResponsiveModels;
using LMT.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMT.Controllers
{
	[Route("[controller]")]

	public class StudentController : GlobalLMTController
	{
		private readonly IStudentService _studentService;
		ResponseData objResponse = new ResponseData();
		Status status = new Status();
		Pagination _pagination = new Pagination();

		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
		}




		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromBody] Student student)
		{
			//var validator = new BooksValidator();
			//ValidationResult results = validator.Validate(book);

			//if (!results.IsValid)
			//{
			//return CreateResponseWithStatus(StatusCodes.Status400BadRequest, GetValidationErrors(results.Errors));
			//}
			student.CreatedBy = 1;
			student.CreatedDate = DateTime.Now;
			status = await _studentService.CreateAsync(student);
			return CreateResponseWithStatus(status.Code, status);

		}


		[HttpGet("search")]
		public async Task<IActionResult> SearchAsync(Status status, Student student, DateRangeFilter dateRange, SortingData sortData)
		{
			var results = await _studentService.SearchAsync( status, student, dateRange, sortData);
			_pagination = new Pagination()
			{
				PageSize = (sortData.PageSize != 0) ? sortData.PageSize : DefaultPageSize.PageSize,
				PageNumber = (sortData.PageNumber != 0) ? sortData.PageNumber : 1,
				TotalRows = results != null ? results.TotalRows : 0
			};
			return CreateResponseWithStatus(status.Code, objResponse.ResponseModel(results.Rows, status, _pagination));
		}
	}
}