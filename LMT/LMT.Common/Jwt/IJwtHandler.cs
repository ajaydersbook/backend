using System;
using System.Collections.Generic;
using System.Text;

namespace LMT.Common.Jwt
{
    public interface IJwtHandler
    {
        JsonWebToken Create(/*int userId,*/ string Email, string UserName);
    }
}
