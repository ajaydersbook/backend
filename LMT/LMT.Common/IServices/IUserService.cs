using LMT.Administration.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Administration.IServices
{
	public interface IUserService
	{
		Task<object> GetByIDAsync(User user);

		
	}
}
