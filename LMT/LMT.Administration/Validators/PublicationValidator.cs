using FluentValidation;
using LMT.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Validators
{
	public class PublicationValidator:AbstractValidator<Publication>
	{
		public PublicationValidator()
		{
			RuleFor(publication => publication.PublicationName).NotNull().NotEmpty();
			RuleFor(publication => publication.PublicationName).NotEmpty().Matches(@"^[a-zA-Z\s]+$").WithMessage("Enter only charecters");
		
		}

	}
}
