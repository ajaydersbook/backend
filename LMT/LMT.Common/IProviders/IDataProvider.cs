using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.IProviders
{
	public interface IDataProvider
	{

		SqlConnection Connection();

		Task<T> ExecuteScalarAsync<T>(string usp, DynamicParameters dynamicParameters);
		Task<dynamic> ExecuteNonQueryAsync(string usp, DynamicParameters dynamicParameters, bool returnOutput = false);
		Task<IEnumerable<T>> ExecuteReaderAsync<T>(string usp, DynamicParameters dynamicParameters);
		Task<dynamic> ExecuteMultipleQueryAsync(string usp, DynamicParameters dynamicParameters, IEnumerable<MapItem> mapItems);
		Task<dynamic> ExecuteReaderMultipleAsync(string usp, DynamicParameters dynamicParameters, IEnumerable<MapItem> mapItems);
	}
}
