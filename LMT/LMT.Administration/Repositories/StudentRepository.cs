using Dapper;
using LMT.IRepositories;
using LMT.Models;
using LMT.Providers;
using LMT.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Repositories
{
	public class StudentRepository: IStudentRepository
	{
		private DataProvider _dataProvider = new DataProvider();

		public async Task<Status> CreateAsync(Student student)
		{
			Status status = new Status();
			try
			{
				var parameters = new DynamicParameters();

				parameters.Add("@in_DML", "Create", dbType: DbType.String, direction: ParameterDirection.Input, size: 10);
				parameters.Add("@in_StudentName", student.StudentName, dbType: DbType.String, direction: ParameterDirection.Input, size: 50);
				parameters.Add("@in_BranchID", student.BranchID, dbType: DbType.Int32, direction: ParameterDirection.Input, size: 5);
				parameters.Add("@in_Gender", student.Gender, dbType: DbType.String, direction: ParameterDirection.Input, size: 1);
				parameters.Add("@in_Mobile", student.Mobile, dbType: DbType.String, direction: ParameterDirection.Input, size: 10);
				parameters.Add("@in_DateOfBirth", student.DateOfBirth, dbType: DbType.DateTime, direction: ParameterDirection.Input, size: 50);
				parameters.Add("@in_City", student.City, dbType: DbType.String, direction: ParameterDirection.Input, size: 50);
				parameters.Add("@in_PinCode", student.Pincode, dbType: DbType.Int32, direction: ParameterDirection.Input, size: 50);
				parameters.Add("@in_Address", student.Address, dbType: DbType.String, direction: ParameterDirection.Input, size: 500);
				parameters.Add("@in_Email", student.Email, dbType: DbType.String, direction: ParameterDirection.Input, size: 50);
				parameters.Add("@in_Password", student.Password, dbType: DbType.String, direction: ParameterDirection.Input, size: 100);
				parameters.Add("@in_CreatedBy", student.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input, size: 15);
				parameters.Add("@in_CreatedDate", student.CreatedDate, dbType: DbType.DateTime, direction: ParameterDirection.Input, size: 20);
				return await _dataProvider.ExecuteNonQueryAsync("LMS_USP_Student", parameters);

			}

			catch (Exception ex)
			{
				status.Code = -1;
				status.Message = ex.Message;
			}
			return status;
		}

		public async Task<List<Student>> SearchAsync(Student student, Status status,  DateRangeFilter dateRange, SortingData sortData)
		{
			var publications = new List<Student>();
			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("@in_DML", "Search", dbType: DbType.String, direction: ParameterDirection.Input, size: 10);
				if (student.ID > 0)
				{
					parameters.Add("@in_ID", student.ID, dbType: DbType.Int32, direction: ParameterDirection.Input, size: 10);
				}
				if (!string.IsNullOrEmpty((student.StudentName)))
				{
					parameters.Add("@in_StudentName", student.StudentName, dbType: DbType.String, direction: ParameterDirection.Input, size: 120);
				}

				if (student.BranchID > 0)
				{
					parameters.Add("@in_BranchID", student.BranchID, dbType: DbType.Int32, direction: ParameterDirection.Input, size: 10);
				}

				if (student.CreatedBy > 0)
				{
					parameters.Add("@in_CreatedBy", student.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input, size: 10);
				}

				if (dateRange.CreatedDateStart != null && dateRange.CreatedDateEnd != null)
				{
					parameters.Add("@in_CreatedDateStart", dateRange.CreatedDateStart, dbType: DbType.DateTime, direction: ParameterDirection.Input, size: 20);
					parameters.Add("@in_CreatedDateEnd", dateRange.CreatedDateEnd, dbType: DbType.DateTime, direction: ParameterDirection.Input, size: 20);
				}
				if (dateRange.UpdatedDateStart != null && dateRange.UpdatedDateEnd != null)
				{
					parameters.Add("@in_UpdatedDateStart", dateRange.UpdatedDateStart, dbType: DbType.DateTime, direction: ParameterDirection.Input, size: 20);
					parameters.Add("@in_UpdatedDateEnd", dateRange.UpdatedDateEnd, dbType: DbType.DateTime, direction: ParameterDirection.Input, size: 20);
				}

				if (sortData.PageNumber > 0)
				{
					parameters.Add("@in_PageNumber", sortData.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input, size: 10);
				}

				if (sortData.PageSize > 0)
				{
					parameters.Add("@in_PageSize", sortData.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input, size: 10);
				}
				if (sortData.SortColumn != null && sortData.SortColumn != "")
				{
					parameters.Add("@in_SortColumn", sortData.SortColumn, dbType: DbType.String, direction: ParameterDirection.Input, size: 20);
				}
				if (sortData.SortOrder != null && sortData.SortOrder != "")
				{
					parameters.Add("@in_SortOrder", sortData.SortOrder, dbType: DbType.String, direction: ParameterDirection.Input, size: 20);
				}


				publications = (await _dataProvider.ExecuteReaderAsync<Student>("[LMS_USP_Student]", parameters)).ToList();

				status.Code = parameters.Get<int>("@ou_ResultNo");
				status.Message = parameters.Get<string>("@ou_ResultMessage");

			}
			catch (Exception ex)
			{
				status.Code = 500;
				status.Message = ex.Message;
			}
			return publications;
		}


	}
}
