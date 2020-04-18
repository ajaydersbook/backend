using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMT.Common.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LMT.Administration.IServices;
using LMT.Administration.ResponsiveModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using LMT.Common.IServices;
using LMT.Models;

namespace LMT.Services.Auth.Controllers
{
    [Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class LoginController : ApiController
    {
        //public IValidator<AuthenticateUser> validator { get; set; }

        //Status _status = new Status();

        private readonly IJwtHandler _jwtHandler;
		private readonly IUserService _userService;
		private readonly IUserCreateService _userCreateService;
		//private readonly IJwtTokenRepository _jwtTokenRepository;
		//private readonly IUserGuestService _userGuestService;
		//Commented due to authorization issue
		//private readonly IUserGroupService _userGroupService;
		// private readonly ISecurity _security;

		public LoginController(IJwtHandler jwtHandler ,IUserService userService, IUserCreateService userCreateService/*, IJwtTokenRepository jwtTokenRepository, IUserGuestService userGuestService, /*IUserGroupService userGroupService, ISecurity security */)
        {
            _jwtHandler = jwtHandler;
			_userService = userService;
			_userCreateService = userCreateService;
			//_jwtTokenRepository = jwtTokenRepository;
			//_userGuestService = userGuestService;
			//Commented due to authorization issue
			//_userGroupService = userGroupService;
			//_security = security;
		}
		//public class AuthenticateUser
		//{
		//	public string EmployeeID { get; set; }
		//	public string Password { get; set; }
		//	public string UserName { get; set; }
		//	public string groupId { get; set; }
		//}
		[HttpOptions]
        //[ResponseType(typeof(void))]
        public IActionResult Options()
        {
            Request.HttpContext.Response.Headers.Add("Allow", "POST,OPTIONS");
            return Ok();
        }
		[AllowAnonymous]
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
           // var validator = new AuthenticateUserValidator();
            //ValidationResult results = validator.Validate(user);

            //if (!results.IsValid)
            //{
            //    return CreateResponseWithStatus(StatusCodes.Status400BadRequest, GetValidationErrors(results.Errors));
            //}

            //var result = await _userGuestService.GetByUserNameAsync(user.EmployeeID, _status);
            if (user == null)
            {
                return CreateResponseWithStatus(StatusCodes.Status400BadRequest, new
                {
                    Code = StatusCodes.Status400BadRequest,
                    Type = "authentication-failed",
                    Message = "Invalid credentials"
                });
            }


            //Password
            //password validation
            //string Passowrd = user.Password;
            //string notEncryptedText = _security.EncryptionKey();
            //string encryptedPass = _security.Encrypt(Passowrd, notEncryptedText);
            //IsDeleted not being used if usergroup getbyID is commented due to authorization issue
            //bool IsDeleted = false;


           // if (encryptedPass == result.Password)
		   if(user != null)
            {
                // dynamic userDetails = new ExpandoObject();
                var userDetails = new User();
				Status status = new Status();
				userDetails = await _userCreateService.ValidateUser(user, status);

				if (status.Code == 200)
				{

					JsonWebToken jwt = _jwtHandler.Create(userDetails.Email, userDetails.UserName);
					//jsonwebtoken jwt = _jwthandler.create(/*user.id,*/ data.name, data.groupname);
					//        await _jwttokenrepository.addtoken(new jwttoken
					//        {
					//            user_id = result.username,
					//            refresh_token = jwt.refreshtoken,
					//            expires = jwt.refreshtokenexpires
					//        });

					//Commented due to authorization issue
					//var result1 = await _userGroupService.GetByIDAsync(Convert.ToInt32(result.GroupID), _status, IsDeleted);
					//Commented end due to authorization issue

					//var result1 = new List<UserGroupPrivilages>();
					//var obj = new UserGroupPrivilages
					//{
					//    GroupID = 1,
					//    ModuleName = "test"
					//};

					//result1.Add(obj);
					//result1.Add(obj);

					return Json(new
					{
						Code = StatusCodes.Status200OK,
						Payload = jwt,
						User = new
						{

							//ID = user.ID,
							Email = userDetails.Email,

							//Name = result.GuestName,
							UserName = userDetails.UserName,
							//Status = result.Status,
						}
					});
					//return Json(new
					//{
					//    Code = StatusCodes.Status200OK,
					//    User = new {
					//        Data = userDetails
					//    }
					//});
				}else
				{
					return CreateResponseWithStatus(StatusCodes.Status400BadRequest, new
					{
						Code = StatusCodes.Status400BadRequest,
						Type = "authentication-failed",
						Message = "Invalid credentials"
					});
				}
				
            }

            return CreateResponseWithStatus(StatusCodes.Status400BadRequest, new
            {
                Code = StatusCodes.Status400BadRequest,
                Type = "authentication-failed",
                Message = "Invalid credentials"
            });
        }
		[Authorize]
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1", "value2" };
		}
		[HttpGet("search")]
		public ActionResult<IEnumerable<string>> search()
		{
			return new string[] { "value1", "value2" };
		}
	}
}