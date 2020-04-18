using FluentValidation;
using LMT.ResponsiveModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Validators
{
	public class BooksValidator:AbstractValidator<Book>
	{
		public BooksValidator()
		{
			RuleFor(book => book.PublicationID).NotNull();
			RuleFor(book => book.BookName).NotEmpty().WithMessage("Should not be empty");

		}

	}
}
