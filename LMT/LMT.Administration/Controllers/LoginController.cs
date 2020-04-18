using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LMT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LMT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
		[HttpPost]
		public  IActionResult LoginUser([FromBody]Login login)
		{
				if(login!=null&& (login.UserName=="Murari" && login.PassWord== "Murari"))
				{
					var tokenDescriptor = new SecurityTokenDescriptor
					{
						Subject = new ClaimsIdentity(new Claim[]
						{
							new Claim("UserName",login.UserName.ToString())
						}),
						Expires = DateTime.Now.AddDays(1),
						SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Murari9704191679")), SecurityAlgorithms.HmacSha256Signature)

					};
					var tokenHandler = new JwtSecurityTokenHandler();
					var securtiyToken = tokenHandler.CreateToken(tokenDescriptor);
					var token = tokenHandler.WriteToken(securtiyToken);
				    return Ok( new { token });
				}
				else
				{
					return BadRequest(new { message = "Unauthorized" });
				}
		}
    }
}