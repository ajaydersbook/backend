using System;
using System.Collections.Generic;
using System.Text;

namespace LMT.Common.Jwt
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public string RefreshToken { get; set; }
        public long RefreshTokenExpires { get; set; }
    }
}
