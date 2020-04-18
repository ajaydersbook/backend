using Dapper;
using LMT.IProviders;
using LMT.Models;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Providers
{
	public class DataProvider
	{
		//private readonly DataProviderOptions _dataAccess;
		//public DataProvider(IOptions<DataProviderOptions> dataAccess)
		//{
		//	_dataAccess = dataAccess.Value;
		//}

		public SqlConnection Connection()
		{
			return new SqlConnection(@"Server = 94.73.148.5; Database = u08_dersbo2; User Id = u08_dersbo2; Password = SuuS142536142536");
		}

		//public OracleConnection Connection()
		//{
		//	//var connectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
		//	var connectionString = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.138.108.170)(PORT = 1522))(CONNECT_DATA = (SID = fcwork))); User Id = focis; Password = focis";
		//	var conn = new OracleConnection(connectionString);
		//	return conn;
		//}
		/// <summary>
		/// Return single record
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="usp"></param>
		/// <param name="dynamicParameters"></param>
		/// <returns></returns>
		public async Task<T> ExecuteScalarAsync<T>(string usp, DynamicParameters dynamicParameters)
		{
			using (var sqlConnection = Connection())
			{
				try
				{
					dynamicParameters.Add(name: "@ou_ResultNo", dbType: DbType.Int32, direction: ParameterDirection.Output);
					dynamicParameters.Add(name: "@ou_ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

					await sqlConnection.OpenAsync();
					var orderDetail = sqlConnection.QueryFirstOrDefaultAsync<T>(
						usp,
						dynamicParameters,
						null,
						null,
						commandType: CommandType.StoredProcedure).Result;

					return orderDetail;

				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message.ToString());
				}
				finally
				{
					sqlConnection.Close();
				}
			}
		}

		/// <summary>
		/// Multiple records
		/// </summary>
		/// <param name="usp"></param>
		/// <param name="dynamicParameters"></param>
		/// <returns></returns>
		public async Task<dynamic> ExecuteMultipleQueryAsync(string usp, DynamicParameters dynamicParameters, IEnumerable<MapItem> mapItems = null)
		{
			var data = new ExpandoObject();

			using (var sqlConnection = Connection())
			{
				dynamicParameters.Add(name: "@ou_ResultNo", dbType: DbType.Int32, direction: ParameterDirection.Output);
				dynamicParameters.Add(name: "@ou_ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

				await sqlConnection.OpenAsync();

				using (var multi = await sqlConnection.QueryMultipleAsync(usp, dynamicParameters, null, null, commandType: CommandType.StoredProcedure))
				{
					if (mapItems == null)
					{
						return data;
					}

					var resultNo = dynamicParameters.Get<dynamic>("@ou_ResultNo");


					if (resultNo == null || resultNo != 404)
					{
						foreach (var item in mapItems)
						{

							if (item.DataRetriveType == DataRetriveType.FirstOrDefault)
							{
								var singleItem = multi.Read(item.Type).FirstOrDefault();
								((IDictionary<string, Object>)data).Add(item.PropertyName, singleItem);
							}
							if (item.DataRetriveType == DataRetriveType.List)
							{
								var listItem = multi.Read(item.Type).ToList();
								((IDictionary<string, Object>)data).Add(item.PropertyName, listItem);
							}
						}
					}

					return data;
				}
			}

		}

		/// <summary>
		/// Executes create, update or delete
		/// </summary>
		/// <param name="usp"></param>
		/// <param name="dynamicParameters"></param>
		/// <returns></returns>
		public async Task<dynamic> ExecuteNonQueryAsync(string usp, DynamicParameters dynamicParameters, bool returnOutput = false)
		{
			using (var sqlConnection = Connection())
			{
				await sqlConnection.OpenAsync();
				dynamicParameters.Add(name: "@ou_ResultNo", dbType: DbType.Int32, direction: ParameterDirection.Output);
				dynamicParameters.Add(name: "@ou_ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

				var affectedRows = await sqlConnection.ExecuteAsync(
					usp,
					dynamicParameters,
					commandType: CommandType.StoredProcedure);

				if (!returnOutput)
				{
					return new Status
					{
						Code = dynamicParameters.Get<int>("@ou_ResultNo"),
						Message = dynamicParameters.Get<string>("@ou_ResultMessage")
					};
				}
				else
				{
					return dynamicParameters;
				}

			}
		}

		/// <summary>
		/// Return multiple records
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="usp"></param>
		/// <param name="dynamicParameters"></param>
		/// <returns></returns>
		public async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string usp, DynamicParameters dynamicParameters)
		{
			using (var sqlConnection = Connection())
			{
				dynamicParameters.Add(name: "@ou_ResultNo", dbType: DbType.Int32, direction: ParameterDirection.Output);
				dynamicParameters.Add(name: "@ou_ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

				await sqlConnection.OpenAsync();
				// https://dapper-tutorial.net/knowledge-base/34270938/automatically-mapping-output-parameters-with-dapper
				return await sqlConnection.QueryAsync<T>(
					usp,
					dynamicParameters,
					null,
					null,
					commandType: CommandType.StoredProcedure);
			}
		}

		public async Task<dynamic> ExecuteReaderMultipleAsync(string usp, DynamicParameters dynamicParameters, IEnumerable<MapItem> mapItems = null)
		{
			var data = new ExpandoObject();

			using (var sqlConnection = Connection())
			{
				await sqlConnection.OpenAsync();

				using (var multi = await sqlConnection.QueryMultipleAsync(usp, dynamicParameters, null, null, commandType: CommandType.StoredProcedure))
				{
					if (mapItems == null)
					{
						return data;
					}
					foreach (var item in mapItems)
					{
						if (multi.IsConsumed)
						{
							break;
						}
						if (item.DataRetriveType == DataRetriveType.FirstOrDefault)
						{
							var singleItem = multi.Read(item.Type).FirstOrDefault();
							((IDictionary<string, Object>)data).Add(item.PropertyName, singleItem);
						}
						if (item.DataRetriveType == DataRetriveType.List)
						{
							var listItem = multi.Read(item.Type).ToList();
							((IDictionary<string, Object>)data).Add(item.PropertyName, listItem);
						}
					}

					var rowCount = data.FirstOrDefault(x => x.Key == mapItems.ElementAt<MapItem>(0).PropertyName).Value;
					var result = ((IEnumerable)rowCount).Cast<object>().ToList();

					if (result.Count == 0)
					{
						((IDictionary<string, Object>)data).Add("Status", new Status() { Code = 400, Message = "No rows found." });
					}
					else
					{
						((IDictionary<string, Object>)data).Add("Status", new Status() { Code = 200, Message = "Successful" });
					}

					return data;
				}
			}
		}
	}
}
