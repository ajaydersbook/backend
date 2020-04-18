using LMT.Models;
using LMT.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.IRepositories
{
	 public interface IStudentRepository
	{

		Task<Status> CreateAsync(Student student);
		//Task<Status> UpdateAsync(Student student);

		//Task<Student> GetByID(int id, Status status);

		Task<List<Student>> SearchAsync(Student student, Status status, DateRangeFilter dateRangeFilter, SortingData sortingData);

	}
}
