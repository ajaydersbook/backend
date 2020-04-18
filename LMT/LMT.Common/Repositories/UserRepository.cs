using Dapper;
using LMT.Administration.IRepositories;
using LMT.Administration.ResponsiveModels;
using LMT.Models;
using LMT.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Administration.Repositories
{
	public class UserRepository:IUserRepository
	{
		private DataProvider _dataProvider = new DataProvider();

		public async Task<User> GetByIDAsync(User user)
		{
            User userDetails = null;

            Status status=new Status();
			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("@in_DML", "GetByID", dbType: DbType.String, direction: ParameterDirection.Input, size: 10);
				parameters.Add("@in_LoginID", user.LoginID, dbType: DbType.String, direction: ParameterDirection.Input, size: 100);
				parameters.Add("@in_Password", user.Password, dbType: DbType.String, direction: ParameterDirection.Input, size: 100);


				parameters.Add("@ou_ResultNo", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@ou_ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output);

                userDetails = (await _dataProvider.ExecuteScalarAsync<User>("LMS_USP_Users", parameters));

				status.Code = parameters.Get<int>("@ou_ResultNo");
				status.Message = parameters.Get<string>("@ou_ResultMessage");

                

            }
			catch (Exception ex)
			{
				status.Code = 500;
				status.Message = ex.Message;
			}
			return userDetails;
		}

	}
}
