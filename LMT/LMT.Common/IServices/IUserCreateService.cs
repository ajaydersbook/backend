using LMT.Administration.ResponsiveModels;
using LMT.Common.ResponsiveModels;
using LMT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Common.IServices
{
	public interface IUserCreateService
	{
		Task<Status> CreateAsync(UserCreate userCreate);

		Task<User> ValidateUser(User user, Status status);
	}
}
