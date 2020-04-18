using LMT.IRepositories;
using LMT.IServices;
using LMT.Models;
using LMT.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Services
{
	public class StudentService:IStudentService
	{

		private readonly IStudentRepository _studentRepository;

		public StudentService(IStudentRepository studentRepository)
		{
			_studentRepository = studentRepository;
		}

		public async Task<Status> CreateAsync(Student student)
		{
			return await _studentRepository.CreateAsync(student);
		}

		public async Task<SearchResult> SearchAsync(Status status, Student student, DateRangeFilter dateRange, SortingData sortData)
		{
			var books = await _studentRepository.SearchAsync(student, status,  dateRange, sortData);
			
			List<object> rows = null;
			rows = books.Select(a => new
			{
				a.ID,
				a.StudentName,
				a.BranchID,
				a.BranchName,
				Gender=(a.Gender=="M")?"Male":"Female",
				a.Mobile,
				//a.DateOfBirth,
				DateOfBirth = a.DateOfBirth.ToString("dddd, dd MMMM yyyy"),
				a.City,
				a.Pincode,
				a.Email,
				a.Password,
				a.Address,
				a.CreatedBy,
				a.CreatedByName,
				a.CreatedDate,
				a.UpdatedBy,
				a.UpdatedByName,
				a.UpdatedDate,
				a.ApprovedBy,
				a.ApprovedByName,
				a.ApprovedDate,

			}).ToList<object>();

			return new SearchResult
			{
				Rows = rows,
				TotalRows = books.FirstOrDefault() != null ? books.FirstOrDefault().TotalCount : 0
			};
		}
	}
}
