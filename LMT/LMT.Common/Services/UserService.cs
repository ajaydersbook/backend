using LMT.Administration.IRepositories;
using LMT.Administration.IServices;
using LMT.Administration.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Administration.Services
{
	public class UserService:IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<object> GetByIDAsync(User user)
		{
			var result = await _userRepository.GetByIDAsync(user);
			//return result;
			if (result != null)
			{
				dynamic userDetails = new ExpandoObject();
				userDetails.Name = result.Name;
				userDetails.LoginID = result.LoginID;
				userDetails.GroupName = result.GroupName;
				
				return result;

			}
			return result;
		}
	}
}
