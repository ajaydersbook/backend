using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMT.Administration.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class ApiController : Controller
    {
        [HttpOptions]
        public IActionResult Options()
        {
            Request.HttpContext.Response.Headers.Add("Allow", "GET,POST,PUT,DELETE,OPTIONS");
            return Ok();
        }

        public object GetValidationErrors(IList<ValidationFailure> errors)
        {
            Dictionary<string, string> _error = null;
            var errorList = new Dictionary<string, Dictionary<string, string>>();
            foreach (var error in errors)
            {
                _error = new Dictionary<string, string>();
                if (!errorList.ContainsKey(error.PropertyName))
                {
                    _error.Add(error.ErrorCode, error.ErrorMessage);
                    errorList.Add(error.PropertyName, _error);
                }
                else
                {
                    _error = errorList[error.PropertyName];
                    _error.Add(error.ErrorCode, error.ErrorMessage);
                    errorList[error.PropertyName] = _error;
                }
            }

            var validationError = new {
                Code = StatusCodes.Status400BadRequest,
                Type = "validation-error",
                Errors = errorList
            };
            return validationError;
        }

        public IActionResult CreateResponseWithStatus(int code, object data)
        {
            switch (code)
            {
                case 200:
                    return StatusCode(StatusCodes.Status200OK, data); //No need to mention explicitly
                case 201:
                    return StatusCode(StatusCodes.Status201Created, data);
                case 202:
                    return StatusCode(StatusCodes.Status202Accepted, data);
                case 204:
                    return StatusCode(StatusCodes.Status204NoContent, data); //For void functions no need to mention status
                case 400:
                    return StatusCode(StatusCodes.Status400BadRequest, data);
                case 401:
                    return StatusCode(StatusCodes.Status401Unauthorized, data);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, data);
                case 409:
                    return StatusCode(StatusCodes.Status409Conflict, data);
                case 501:
                    return StatusCode(StatusCodes.Status501NotImplemented, data);
                default:
                    return null;
            }
        }
        

        
    }
}