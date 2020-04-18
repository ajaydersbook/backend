using LMT.Models;
using LMT.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.IServices
{
	public interface IStudentService
	{

		Task<Status> CreateAsync(Student student);

		//Task<Status> UpdateAsync(Student student);

		//Task<object> GetByIDAsync(int id, Status status);

		Task<SearchResult> SearchAsync(Status status, Student student, DateRangeFilter dateRange, SortingData sortData);


	}
}
