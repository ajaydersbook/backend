using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Models
{
	public class ResponseData
	{
		public string Code { get; set; }
		public string Message { get; set; }

		public ResponseModel ResponseModel(object data, Status status, Pagination paging)
		{
			if (status.Code == 500)
			{
				status.Message = "Internal server error.";
				return new ResponseModel()
				{
					status = status
				};
			}
			return new ResponseModel()
			{
				data = data,
				status = status,
				pagination = paging
			};
		}


	}
}
