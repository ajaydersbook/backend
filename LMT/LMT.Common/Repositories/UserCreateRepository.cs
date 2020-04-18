using Dapper;
using LMT.Administration.ResponsiveModels;
using LMT.Common.Encrypter;
using LMT.Common.IRepositories;
using LMT.Common.ResponsiveModels;
using LMT.IProviders;
using LMT.Models;
using LMT.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Common.Repositories
{
	public class UserCreateRepository: IUserCreateRepository
	{
		private DataProvider _dataProvider = new DataProvider();

		Security _security = new Security();

		public async Task<Status> CreateAsync(UserCreate usercreate)
		{
			Status status = new Status();
			try
			{
				var parameters = new DynamicParameters();

				string passowrd = usercreate.Password;
				string notEncryptedText = _security.EncryptionKey();
				string encryptedPass = _security.Encrypt(passowrd, notEncryptedText);

				parameters.Add("@in_DML", "Create", dbType: DbType.String, direction: ParameterDirection.Input, size: 10);
				parameters.Add("@in_Email", usercreate.Email, dbType: DbType.String, direction: ParameterDirection.Input, size: 100);
				parameters.Add("@in_UserName", usercreate.UserName.ToLower(), dbType: DbType.String, direction: ParameterDirection.Input, size: 50);
				parameters.Add("@in_Password", encryptedPass, dbType: DbType.String, direction: ParameterDirection.Input, size: 3000);

				status = await _dataProvider.ExecuteNonQueryAsync("DBook_USP_ADMIN_User", parameters);

			}
			catch (Exception ex)
			{

				status.Code = -1;
				status.Message = ex.Message;
			}
			return status;
		}


		public async Task<User> validateUser(User user, Status status)
		{
			User userCreate = null;
			//Status status=new Status();
			try
			{
				var parameters = new DynamicParameters();
				string passowrd = user.Password;
				string notEncryptedText = _security.EncryptionKey();
				string encryptedPass = _security.Encrypt(passowrd, notEncryptedText);

				parameters.Add("@in_DML", "GetByID", dbType: DbType.String, direction: ParameterDirection.Input, size: 10);
				parameters.Add("@in_Email", user.Email, dbType: DbType.String, direction: ParameterDirection.Input, size: 100);
				parameters.Add("@in_Password", encryptedPass, dbType: DbType.String, direction: ParameterDirection.Input, size: 3000);


				parameters.Add("@ou_ResultNo", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@ou_ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output);

				userCreate = await _dataProvider.ExecuteScalarAsync<User>("DBook_USP_ADMIN_User", parameters);

				status.Code = parameters.Get<int>("@ou_ResultNo");
				status.Message = parameters.Get<string>("@ou_ResultMessage");

			}
			catch (Exception ex)
			{
				status.Code = 500;
				status.Message = ex.Message;
			}
			return userCreate;
		}
	}
}
