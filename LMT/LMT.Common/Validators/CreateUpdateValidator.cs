using LMT.Common.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;


namespace LMT.Common.Validators
{
	public class CreateUpdateValidator : AbstractValidator<UserCreate>
	{


		public CreateUpdateValidator()
		{
			RuleFor(user => user.Email).NotEmpty().EmailAddress();
			RuleFor(user => user.UserName).NotEmpty().MinimumLength(3).MaximumLength(50).Matches(RegExs.Username).WithMessage("Please use only letters (a-z), numbers and periods.").When(user => user.ID == 0);
			RuleFor(user => user.Password).NotEmpty().When(user => !string.IsNullOrEmpty(user.Password)).Matches(RegExs.PasswordPolicy).When(user => user.Password != null).WithMessage("Passwords must contain at least 8 characters, including uppercase, lowercase letters, numbers and special characters.").When(user => user.ID == 0);
		}
	}
}
