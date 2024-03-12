using FluentValidation;

namespace SharpOutcome.HttpApiExample.DataTransferObjects;

public class BookRequestValidator : AbstractValidator<BookRequest>
{
    public BookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .MaximumLength(38);

        RuleFor(x => x.Genre)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Author)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Price)
            .NotNull()
            .GreaterThan(0);
    }
}