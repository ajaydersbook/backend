using LMT.Administration.ResponsiveModels;
using LMT.Common.ResponsiveModels;
using LMT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Common.IRepositories
{
	public interface IUserCreateRepository
	{
		Task <Status> CreateAsync(UserCreate userCreate);

		Task<User> validateUser(User user, Status status);
	}
}
