using LMT.Administration.ResponsiveModels;
using LMT.Common.IRepositories;
using LMT.Common.IServices;
using LMT.Common.ResponsiveModels;
using LMT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Common.Services
{
	public class UserCreateService: IUserCreateService
	{
		private readonly IUserCreateRepository _userCreateRepository;

		public UserCreateService(IUserCreateRepository userCreateRepository)
		{
			_userCreateRepository = userCreateRepository;
		}

		public async Task<Status> CreateAsync(UserCreate userCreate)
		{
			return await _userCreateRepository.CreateAsync(userCreate);
		}

		public async Task<User> ValidateUser(User user, Status status)
		{
			return await _userCreateRepository.validateUser(user, status);
		}



	}
}
